using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class PaymentViewModel
    {
        public string Name { get; set; }
        [Display(Name = "Card Number")]
        public string CardNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTimeOffset Date { get; set; }
        [Display(Name ="CVV Number")]
        public int CVV { get; set; }
    }
}
