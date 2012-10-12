using System;
using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BudgetContext context = new BudgetContext();

        private GenericRepository<Budget> budgetRepository;
        private GenericRepository<Envelope> envelopeRepository;
        private GenericRepository<Transaction> transactionRepository;
        private GenericRepository<User> userRepository;

        public GenericRepository<Budget> BudgetRepository
        {
            get
            {
                if (this.budgetRepository == null)
                    this.budgetRepository = new GenericRepository<Budget>(context);

                return budgetRepository;
            }
        }

        public GenericRepository<Envelope> EnvelopeRepository
        {
            get
            {
                if (this.envelopeRepository == null)
                    this.envelopeRepository = new GenericRepository<Envelope>(context);

                return envelopeRepository;
            }
        }

        public GenericRepository<Transaction> TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                    this.transactionRepository = new GenericRepository<Transaction>(context);

                return transactionRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<User>(context);

                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}