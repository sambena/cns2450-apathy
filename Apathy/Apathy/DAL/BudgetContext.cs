using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Apathy.Models
{
    public class BudgetContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Envelope> Envelopes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public void Detach(object entity)
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.Detach(entity);
        }
    }
}