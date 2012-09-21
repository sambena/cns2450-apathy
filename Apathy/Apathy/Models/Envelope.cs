using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class Envelope
    {
        [Key]
        public int EnvelopeID { get; set; }

        public int BudgetID { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        [Required(ErrorMessage = "Starting balance is required.")]
        [Column(TypeName = "money")]
        public decimal StartingBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        [Column(TypeName = "money")]
        public decimal CurrentBalance { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}