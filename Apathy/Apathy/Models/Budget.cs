using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class Budget
    {
        [Key]
        public Guid BudgetID { get; set; }

        public virtual ICollection<Envelope> Envelopes { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Budget()
        {
            this.BudgetID = Guid.NewGuid();
            this.Envelopes = new List<Envelope>();
            this.Users = new List<User>();
        }
    }
}