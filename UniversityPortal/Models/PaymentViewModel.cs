using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class PaymentViewModel
    {

        [Display(Name = "Card Number")]
        public string CardNo { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTimeOffset? Date { get; set; }
        [Display(Name = "CVV Number")]
        public int CVV { get; set; }
        public string Name { get; set; } = null!;

        public string FeesType { get; set; } = null!;
        public int Sem { get; set; }
        public int Year { get; set; }
    }
}
