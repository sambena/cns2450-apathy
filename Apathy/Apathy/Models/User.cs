using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        [ForeignKey("Budget")]
        public Guid BudgetID { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public User()
        {
            this.Transactions = new List<Transaction>();
        }
    }
}