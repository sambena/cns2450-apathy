using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IEnvelopeRepository
    {
        Envelope GetById(int id);
        IEnumerable<Envelope> GetUserEnvelopes(string user);
        Envelope Add(Envelope envelope);
        void Update(Envelope envelope);
        Envelope Remove(Envelope envelope);
        Envelope Remove(int id);
    }

    public class EnvelopeRepository : IEnvelopeRepository
    {
        private BudgetContext context;

        public EnvelopeRepository(BudgetContext context)
        {
            this.context = context;
        }

        public Envelope GetById(int id)
        {
            return context.Envelopes.Find(id);
        }

        public IEnumerable<Envelope> GetUserEnvelopes(string user)
        {
            return context.Budgets
                .Single(b => b.Owner == user)
                .Envelopes;
        }

        public Envelope Add(Envelope envelope)
        {
            envelope.CurrentBalance = envelope.StartingBalance;
            return context.Envelopes.Add(envelope);
        }

        public void Update(Envelope envelope)
        {
            Envelope current = GetById(envelope.EnvelopeID);
            envelope.BudgetID = current.BudgetID;
            envelope.CurrentBalance = current.CurrentBalance;

            decimal difference;
            if ((difference = envelope.StartingBalance - current.StartingBalance) != 0)
                envelope.CurrentBalance += difference;

            context.Entry(current).CurrentValues.SetValues(envelope);
        }

        public Envelope Remove(Envelope envelope)
        {
            return context.Envelopes.Remove(envelope);
        }

        public Envelope Remove(int id)
        {
            return context.Envelopes.Remove(GetById(id));
        }
    }
}