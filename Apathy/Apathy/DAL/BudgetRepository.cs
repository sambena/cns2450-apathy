using System.Data;
using System.Linq;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IBudgetRepository
    {
        Budget GetByOwner(string owner);
        Budget Create(string owner);
        void Update(Budget budget);
        Budget Remove(Budget budget);
        Budget Remove(string owner);
    }

    public class BudgetRepository : IBudgetRepository
    {
        private BudgetContext context;

        public BudgetRepository(BudgetContext context)
        {
            this.context = context;
        }

        public Budget GetByOwner(string owner)
        {
            return context.Budgets.SingleOrDefault(b => b.Owner == owner);
        }

        public Budget Create(string owner)
        {
            return context.Budgets.Add(new Budget { Owner = owner });
        }

        public void Update(Budget budget)
        {
            context.Entry(budget).State = EntityState.Modified;
        }

        public Budget Remove(Budget budget)
        {
            return context.Budgets.Remove(budget);
        }

        public Budget Remove(string owner)
        {
            return context.Budgets.Remove(GetByOwner(owner));
        }
    }
}