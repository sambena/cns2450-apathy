using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface ITransactionRepository
    {
        Transaction GetById(int id);
        IEnumerable<Transaction> GetUserTransactions(string user);
        IEnumerable<Transaction> GetRecentTransactions(string user, int days, int count);
        Transaction Add(Transaction transaction);
        void Update(Transaction transaction);
        Transaction Remove(int id);
        Transaction Remove(Transaction transaction);
    }

    public class TransactionRepository : ITransactionRepository
    {
        private BudgetContext context = new BudgetContext();

        public TransactionRepository(BudgetContext context)
        {
            this.context = context;
        }

        public Transaction GetById(int id)
        {
            return context.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetUserTransactions(string user)
        {
            return context.Budgets
                .Single(b => b.Owner == user)
                .Envelopes
                .SelectMany(e => e.Transactions);
        }

        public IEnumerable<Transaction> GetRecentTransactions(string user, int days, int count)
        {
            return context.Budgets
                .Single(b => b.Owner == user)
                .Envelopes
                .SelectMany(e => e.Transactions)
                .Where(t => t.Date > DateTime.Today.AddDays(-days))
                .OrderByDescending(t => t.Date)
                .Take(count);
        }

        public Transaction Add(Transaction transaction)
        {
            Envelope envelope = context.Envelopes.Find(transaction.EnvelopeID);
            envelope.CurrentBalance -= transaction.Amount;
            context.Entry(envelope).State = EntityState.Modified;

            return context.Transactions.Add(transaction);
        }

        public void Update(Transaction transaction)
        {
            context.Entry(transaction).State = EntityState.Modified;
        }

        public Transaction Remove(int id)
        {
            return Remove(context.Transactions.Find(id));
        }

        public Transaction Remove(Transaction transaction)
        {
            transaction.Envelope.CurrentBalance += transaction.Amount;
            context.Entry(transaction.Envelope).State = EntityState.Modified;

            return context.Transactions.Remove(transaction);
        }
    }
}