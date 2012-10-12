using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public class TransactionService
    {
        private UnitOfWork uow;

        public TransactionService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Transaction GetTransactionByID(int id)
        {
            return uow.TransactionRepository.GetByID(id);
        }

        public void InsertTransaction(Transaction transaction)
        {
            Envelope envelope = uow.EnvelopeRepository.GetByID(transaction.EnvelopeID);
            envelope.CurrentBalance -= transaction.Amount;

            uow.TransactionRepository.Insert(transaction);
            uow.Save();
        }

        public void DeleteTransaction(int id)
        {
            DeleteTransaction(uow.TransactionRepository.GetByID(id));
        }

        public void DeleteTransaction(Transaction transaction)
        {
            Envelope envelope = uow.EnvelopeRepository.GetByID(transaction.EnvelopeID);
            envelope.CurrentBalance += transaction.Amount;

            uow.TransactionRepository.Delete(transaction);
            uow.Save();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            Transaction transactionBeforeUpdate = uow.TransactionRepository.GetByID(transaction.TransactionID);
            Envelope newEnvelope = uow.EnvelopeRepository.GetByID(transaction.EnvelopeID);

            transactionBeforeUpdate.Envelope.CurrentBalance += transactionBeforeUpdate.Amount;
            newEnvelope.CurrentBalance -= transaction.Amount;

            uow.TransactionRepository.Detach(transactionBeforeUpdate);
            uow.TransactionRepository.Update(transaction);
            uow.Save();
        }

        public IEnumerable<Transaction> GetUserTransactions(string userName)
        {
            var transactions = uow.BudgetRepository.Get(includeProperties: "Envelopes",
                filter: b => b.Owner.Equals(userName)).Single()
                .Envelopes
                .SelectMany(e => e.Transactions);

            return transactions.ToList();
        }
    }
}