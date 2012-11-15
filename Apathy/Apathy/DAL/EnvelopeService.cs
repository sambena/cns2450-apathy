using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IEnvelopeService
    {
        Envelope GetEnvelope(int envelopeID, string username);
        IEnumerable<Envelope> GetEnvelopes(string username);
        void InsertEnvelope(Envelope envelope, string username);
        void UpdateEnvelope(Envelope envelope);
        void DeleteEnvelope(Envelope envelope);
        void DeleteEnvelope(int envelopeID, string username);
        void ResetEnvelope(int envelopeID, string username);
        void ResetEnvelope(Envelope envelope);
        void ResetAllEnvelopes(string username);
    }

    public class EnvelopeService : IEnvelopeService
    {
        private UnitOfWork uow;

        public EnvelopeService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Envelope GetEnvelope(int envelopeID, string username)
        {
            Guid budgetID = uow.UserRepository.GetByPK(username).BudgetID;
            Envelope envelope = uow.EnvelopeRepository.GetByPK(envelopeID);

            // Make sure object exists and user has access
            if (envelope == null || envelope.BudgetID != budgetID)
                return null;

            return envelope;
        }

        public IEnumerable<Envelope> GetEnvelopes(string username)
        {
            User user = uow.UserRepository.GetByPK(username);

            if (user == null)
                return null;     

            return user.Budget.Envelopes;
        }

        public void InsertEnvelope(Envelope envelope, string username)
        {
            envelope.BudgetID       = uow.UserRepository.GetByPK(username).BudgetID;
            envelope.CurrentBalance = envelope.StartingBalance;

            uow.EnvelopeRepository.Insert(envelope);
            uow.Save();
        }

        public void UpdateEnvelope(Envelope envelope)
        {
            Envelope envelopeBeforeUpdate = uow.EnvelopeRepository.GetByPK(envelope.EnvelopeID);

            envelope.BudgetID       = envelopeBeforeUpdate.BudgetID;
            envelope.CurrentBalance = envelopeBeforeUpdate.CurrentBalance;

            decimal difference = envelopeBeforeUpdate.StartingBalance - envelope.StartingBalance;
            envelope.CurrentBalance -= difference;

            uow.EnvelopeRepository.Detach(envelopeBeforeUpdate);
            uow.EnvelopeRepository.Update(envelope);
            uow.Save();
        }

        public void DeleteEnvelope(Envelope envelope)
        {
            uow.EnvelopeRepository.Delete(envelope);
            uow.Save();
        }

        public void DeleteEnvelope(int envelopeID, string username)
        {
            Envelope envelope = GetEnvelope(envelopeID, username);
            DeleteEnvelope(envelope);
            uow.Save();
        }

        public void ResetAllEnvelopes(string username)
        {
            Budget budget = uow.UserRepository.GetByPK(username).Budget;

            foreach (Envelope envelope in budget.Envelopes)
            {
                envelope.CurrentBalance = envelope.StartingBalance;
            }

            uow.Save();
        }

        public void ResetEnvelope(int envelopeID, string username)
        {
            Envelope envelope = GetEnvelope(envelopeID, username);
            ResetEnvelope(envelope);
        }

        public void ResetEnvelope(Envelope envelope)
        {
            envelope.CurrentBalance = envelope.StartingBalance;
            uow.EnvelopeRepository.Update(envelope);
            uow.Save();
        }
    }
}