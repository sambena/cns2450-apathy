using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface ITransactionProcessor
    {
        void Execute();
        void Undo();
    }

    public static class TransactionProcessorFactory
    {
        public static ITransactionProcessor CreateProcessor(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Expense:
                    return new ExpenseProcessor(transaction);
                case TransactionType.Deposit:
                    return new DepositProcessor(transaction);
                default:
                    throw new ArgumentException("Invalid transaction type of " + transaction.Type);
            }
        }
    }

    public class ExpenseProcessor : ITransactionProcessor
    {
        private Transaction transaction;

        public ExpenseProcessor(Transaction transaction)
        {
            this.transaction = transaction;
        }

        public void Execute()
        {
            transaction.Envelope.CurrentBalance -= transaction.Amount;
        }

        public void Undo()
        {
            transaction.Envelope.CurrentBalance += transaction.Amount;
        }
    }

    public class DepositProcessor : ITransactionProcessor
    {
        private Transaction transaction;

        public DepositProcessor(Transaction transaction)
        {
            this.transaction = transaction;
        }

        public void Execute()
        {
            transaction.Envelope.CurrentBalance += transaction.Amount;
        }

        public void Undo()
        {
            transaction.Envelope.CurrentBalance -= transaction.Amount;
        }
    }
}
