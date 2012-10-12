using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public class BudgetService
    {
        private UnitOfWork uow;

        public BudgetService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Budget GetBudgetByID(int id)
        {
            return uow.BudgetRepository.GetByID(id);
        }

        public Budget GetBudgetByUserName(string userName)
        {
            return uow.BudgetRepository.Get(filter: b => b.Owner.Equals(userName)).Single();
        }
    }
}