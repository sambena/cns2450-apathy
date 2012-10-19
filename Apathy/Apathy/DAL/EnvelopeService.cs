using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IEnvelopeService
    {
        Envelope GetEnvelope(int envelopeID);
        IEnumerable<Envelope> GetEnvelopes(string username);
        void InsertEnvelope(Envelope envelope, string username);
        void UpdateEnvelope(Envelope envelope);
        void DeleteEnvelope(Envelope envelope);
        void DeleteEnvelope(int envelopeID);
        void ResetEnvelope(int envelopeID);
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

        public Envelope GetEnvelope(int envelopeID)
        {
            return uow.EnvelopeRepository.GetByPK(envelopeID);
        }

        public IEnumerable<Envelope> GetEnvelopes(string username)
        {
            var envelopes = uow.UserRepository.GetByPK(username)
                .Budget
                .Envelopes;

            return envelopes;
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

            envelope.BudgetID = envelopeBeforeUpdate.BudgetID;
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

        public void DeleteEnvelope(int envelopeID)
        {
            uow.EnvelopeRepository.Delete(envelopeID);
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

        public void ResetEnvelope(int envelopeID)
        {
            ResetEnvelope(uow.EnvelopeRepository.GetByPK(envelopeID));
        }

        public void ResetEnvelope(Envelope envelope)
        {
            envelope.CurrentBalance = envelope.StartingBalance;
            uow.EnvelopeRepository.Update(envelope);
            uow.Save();
        }
    }
}