using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface ITransactionService
    {
        Transaction GetTransaction(int transactionID, string username);
        IEnumerable<Transaction> GetTransactions(string username);
        IEnumerable<Transaction> GetRecentTransactions(string username);
        void InsertTransaction(Transaction transaction, string username);
        void UpdateTransaction(Transaction transaction, string username);
        void DeleteTransaction(int transactionID, string username);
        void DeleteTransaction(Transaction transaction);
    }

    public class TransactionService : ITransactionService
    {
        private UnitOfWork uow;

        public TransactionService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Transaction GetTransaction(int transactionID, string username)
        {
            Guid budgetID = uow.UserRepository.GetByPK(username).BudgetID;
            Transaction transaction = uow.TransactionRepository.GetByPK(transactionID);

            // Make sure object exists and user has access
            if (transaction == null || transaction.Envelope.BudgetID != budgetID)
                throw new HttpException(404, "Resource not found");

            return transaction;
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
            // Use the absolute value of the transaction amount.
            // If user enters -$10.00 for an expense, then we will assume
            //   that the user meant to deduct $10.00 from the envelope.            
            transaction.Amount = Math.Abs(transaction.Amount);

            // Assign values that are not provided by the user
            transaction.UserName    = username;
            transaction.CreatedDate = DateTime.Now;
            transaction.Envelope    = uow.EnvelopeRepository.GetByPK(transaction.EnvelopeID);

            TransactionCommandFactory.CreateCommand(transaction).Execute();
            
            uow.TransactionRepository.Insert(transaction);
            uow.Save();
        }

        public void UpdateTransaction(Transaction transaction, string username)
        {
            Transaction transactionBeforeUpdate = uow.TransactionRepository.GetByPK(transaction.TransactionID);

            transaction.Amount = Math.Abs(transaction.Amount);

            // The transaction object passed to this method
            //  is not the same transaction object before it was modified,
            //  so we need to copy values over from the original object
            transaction.Envelope    = uow.EnvelopeRepository.GetByPK(transaction.EnvelopeID);
            transaction.CreatedDate = transactionBeforeUpdate.CreatedDate;
            transaction.UserName    = username;

            // Undo what the transaction did before it was modified
            TransactionCommandFactory.CreateCommand(transactionBeforeUpdate).Undo();

            // Re-run the transaction with the new changes
            TransactionCommandFactory.CreateCommand(transaction).Execute();

            uow.TransactionRepository.Detach(transactionBeforeUpdate);
            uow.TransactionRepository.Update(transaction);
            uow.Save();
        }

        public void DeleteTransaction(int transactionID, string username)
        {
            Transaction transaction = GetTransaction(transactionID, username);
            DeleteTransaction(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            TransactionCommandFactory.CreateCommand(transaction).Undo();

            uow.TransactionRepository.Delete(transaction);
            uow.Save();
        }
    }
}