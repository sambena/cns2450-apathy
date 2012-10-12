using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
    }
}