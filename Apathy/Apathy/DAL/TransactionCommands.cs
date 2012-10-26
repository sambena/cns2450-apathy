using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface ITransactionCommand
    {
        void Execute();
        void Undo();
    }

    public static class TransactionCommandFactory
    {
        public static ITransactionCommand CreateCommand(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Expense:
                    return new ExpenseCommand(transaction);
                case TransactionType.Deposit:
                    return new DepositCommand(transaction);
                default:
                    throw new ArgumentException("Invalid transaction type of " + transaction.Type);
            }
        }
    }

    public class ExpenseCommand : ITransactionCommand
    {
        private Transaction transaction;

        public ExpenseCommand(Transaction transaction)
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

    public class DepositCommand : ITransactionCommand
    {
        private Transaction transaction;

        public DepositCommand(Transaction transaction)
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
