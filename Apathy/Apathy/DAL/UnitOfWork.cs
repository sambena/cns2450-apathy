using System;
using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BudgetContext context = new BudgetContext();

        private IBudgetRepository budgetRepository;
        private IEnvelopeRepository envelopeRepository;
        private ITransactionRepository transactionRepository;
        private IUserRepository userRepository;

        public IBudgetRepository BudgetRepository
        {
            get
            {
                if (this.budgetRepository == null)
                    this.budgetRepository = new BudgetRepository(context);

                return budgetRepository;
            }
        }

        public IEnvelopeRepository EnvelopeRepository
        {
            get
            {
                if (this.envelopeRepository == null)
                    this.envelopeRepository = new EnvelopeRepository(context);

                return envelopeRepository;
            }
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                    this.transactionRepository = new TransactionRepository(context);

                return transactionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new UserRepository(context);

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