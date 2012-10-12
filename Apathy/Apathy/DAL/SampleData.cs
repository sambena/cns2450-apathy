using System;
using System.Collections.Generic;
using System.Data.Entity;
using Apathy.Models;

namespace Apathy.DAL
{
    public class SampleData : DropCreateDatabaseIfModelChanges<BudgetContext>
    {
        protected override void Seed(BudgetContext context)
        {
            /*
            var budgets = new List<Budget>
            {
                new Budget { Owner = "mptolman" },
                new Budget { Owner = "testuser" }
            };
            budgets.ForEach(b => context.Budgets.Add(b));
            context.SaveChanges();

            var envelopes = new List<Envelope>
            {
                new Envelope { BudgetID = 1, Title = "Gas", StartingBalance = 150, CurrentBalance = 150 },
                new Envelope { BudgetID = 1, Title = "Groceries", StartingBalance = 200, CurrentBalance = 200 },
                new Envelope { BudgetID = 1, Title = "Eating out", StartingBalance = 50, CurrentBalance = 50 },
                new Envelope { BudgetID = 1, Title = "Utilities", StartingBalance = 100, CurrentBalance = 100 },
                new Envelope { BudgetID = 2, Title = "Gas", StartingBalance = 100, CurrentBalance = 100 },
                new Envelope { BudgetID = 2, Title = "Groceries", StartingBalance = 150, CurrentBalance = 150 },
                new Envelope { BudgetID = 2, Title = "Eating out", StartingBalance = 50, CurrentBalance = 50 },
                new Envelope { BudgetID = 2, Title = "Utilities", StartingBalance = 100, CurrentBalance = 100 },
                new Envelope { BudgetID = 2, Title = "Entertainment", StartingBalance = 30, CurrentBalance = 30 }
            };
            envelopes.ForEach(e => context.Envelopes.Add(e));
            context.SaveChanges();

            var transactions = new List<Transaction>
            {
                new Transaction { EnvelopeID = 1, Amount = 50, Date = DateTime.Parse("2012-09-19") },
                new Transaction { EnvelopeID = 3, Amount = 12.25M, Date = DateTime.Parse("2012-09-18") }
            };
            transactions.ForEach(t => context.Transactions.Add(t));
            context.SaveChanges();
            */
        }
    }
}