using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class BudgetUser
    {
        [Key]
        [Column(Order = 0)]
        public int BudgetID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserName { get; set; }

        public virtual Budget Budget { get; set; }
    }
}