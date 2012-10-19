using System;
using System.ComponentModel.DataAnnotations;

namespace Apathy.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("Envelope")]
        public int EnvelopeID { get; set; }

        [ForeignKey("User")]
        public string UserName { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        [Display(Name="Date")]
        public DateTime TransactionDate { get; set; }

        [MaxLength(50)]
        public string Payee { get; set; }

        [MaxLength(1024)]
        public string Notes { get; set; }

        public virtual User User { get; set; }
        public virtual Envelope Envelope { get; set; }
    }
}