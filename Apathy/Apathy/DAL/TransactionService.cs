using System;
using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface ITransactionService
    {
        Transaction GetTransaction(int transactionID);
        IEnumerable<Transaction> GetTransactions(string username);
        IEnumerable<Transaction> GetRecentTransactions(string username);
        void InsertTransaction(Transaction transaction, string username);
        void UpdateTransaction(Transaction transaction, string username);
        void DeleteTransaction(int transactionID);
        void DeleteTransaction(Transaction transaction);
    }

    public class TransactionService : ITransactionService
    {
        private UnitOfWork uow;

        public TransactionService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Transaction GetTransaction(int transactionID)
        {
            return uow.TransactionRepository.GetByPK(transactionID);
        }

        public IEnumerable<Transaction> GetTransactions(string username)
        {
            var transactions = uow.UserRepository.GetByPK(username)
                .Budget
                .Envelopes
                .SelectMany(e => e.Transactions);

            return transactions.ToList();
        }

        public IEnumerable<Transaction> GetRecentTransactions(string username)
        {
            var recentTransactions = GetTransactions(username)
                .Where(t => t.TransactionDate > DateTime.Today.AddDays(-14))
                .Take(5);

            return recentTransactions.ToList();
        }

        public void InsertTransaction(Transaction transaction, string username)
        {
            transaction.UserName    = username;
            transaction.CreatedDate = DateTime.Now;
            transaction.Envelope    = uow.EnvelopeRepository.GetByPK(transaction.EnvelopeID);

            ITransactionProcessor processor = TransactionProcessorFactory.CreateProcessor(transaction);
            processor.Execute();
            
            uow.TransactionRepository.Insert(transaction);
            uow.Save();
        }

        public void UpdateTransaction(Transaction transaction, string username)
        {
            ITransactionProcessor processor;

            Transaction transactionBeforeUpdate = uow.TransactionRepository.GetByPK(transaction.TransactionID);

            transaction.Amount      = Math.Abs(transaction.Amount);
            transaction.Envelope    = uow.EnvelopeRepository.GetByPK(transaction.EnvelopeID);
            transaction.CreatedDate = transactionBeforeUpdate.CreatedDate;
            transaction.UserName    = username;

            processor = TransactionProcessorFactory.CreateProcessor(transactionBeforeUpdate);
            processor.Undo();

            processor = TransactionProcessorFactory.CreateProcessor(transaction);
            processor.Execute();

            uow.TransactionRepository.Detach(transactionBeforeUpdate);
            uow.TransactionRepository.Update(transaction);
            uow.Save();
        }

        public void DeleteTransaction(int transactionID)
        {
            DeleteTransaction(uow.TransactionRepository.GetByPK(transactionID));
        }

        public void DeleteTransaction(Transaction transaction)
        {
            ITransactionProcessor processor;

            processor = TransactionProcessorFactory.CreateProcessor(transaction);
            processor.Undo();

            uow.TransactionRepository.Delete(transaction);
            uow.Save();
        }
    }
}