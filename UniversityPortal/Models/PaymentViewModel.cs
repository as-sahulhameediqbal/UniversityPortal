using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class PaymentViewModel
    {
        public string Name { get; set; } = null!;
        [Display(Name = "Card Number")]
        public int CardNo { get; set; }
        public string? DateMM { get; set; }        
        public int? DateYY { get; set; }
        [Display(Name = "CVV Number")]
        public int CVV { get; set; }
        public string FeesType { get; set; } = null!;
        public int Sem { get; set; }
        public int Year { get; set; }
    }
}
