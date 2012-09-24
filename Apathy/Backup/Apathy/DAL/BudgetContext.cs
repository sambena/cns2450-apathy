using System.Data.Entity;

namespace Apathy.Models
{
    public class BudgetContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Envelope> Envelopes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}