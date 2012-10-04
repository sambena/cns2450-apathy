using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class Budget
    {
        [Key]
        public int BudgetID { get; set; }

        [MaxLength(256)]
        public string Owner { get; set; }

        public virtual ICollection<Envelope> Envelopes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}