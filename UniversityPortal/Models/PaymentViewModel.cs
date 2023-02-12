using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class PaymentViewModel
    {
        [Display(Name = "Card Number")]
        public string CardNo { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset Date { get; set; }
        [Display(Name ="CVV Number")]
        public int CVV { get; set; }
        public decimal Name { get; set; }
    }
}
