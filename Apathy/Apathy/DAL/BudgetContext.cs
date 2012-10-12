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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Users).WithMany(u => u.Budgets)
                .Map(t => t.MapLeftKey("BudgetID")
                    .MapRightKey("UserName")
                    .ToTable("BudgetUser"));
        }

        public void Detach(object entity)
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.Detach(entity);
        }
    }
}