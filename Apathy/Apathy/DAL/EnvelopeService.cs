using System.Collections.Generic;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public class EnvelopeService
    {
        private UnitOfWork uow;

        public EnvelopeService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Envelope GetEnvelopeByID(int id)
        {
            return uow.EnvelopeRepository.GetByID(id);
        }

        public IEnumerable<Envelope> GetUserEnvelopes(string userName)
        {
            var envelopes = uow.BudgetRepository.Get(includeProperties: "Envelopes",
                filter: b => b.Owner.Equals(userName)).Single().Envelopes;

            return envelopes;
        }

        public void InsertEnvelope(string userName, Envelope envelope)
        {
            envelope.BudgetID = uow.BudgetRepository.Get(
                filter: b => b.Owner.Equals(userName)).Single().BudgetID;

            envelope.CurrentBalance = envelope.StartingBalance;

            uow.EnvelopeRepository.Insert(envelope);
            uow.Save();
        }

        public void UpdateEnvelope(Envelope envelope)
        {
            Envelope envelopeBeforeUpdate = uow.EnvelopeRepository.GetByID(envelope.EnvelopeID);

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

        public void DeleteEnvelope(int envelopeId)
        {
            uow.EnvelopeRepository.Delete(envelopeId);
            uow.Save();
        }
    }
}