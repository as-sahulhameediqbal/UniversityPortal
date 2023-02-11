using System.ComponentModel.DataAnnotations;
namespace UniversityPortal.Models
{
    public class FeesViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public int FeeAmount { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}
