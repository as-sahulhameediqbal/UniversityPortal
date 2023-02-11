using System.ComponentModel.DataAnnotations;
namespace UniversityPortal.Models
{
    public class MarksViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public int AdmissionNumber { get; set; }
        [Required]
        public List<SemesterMarksViewModel> SemesterMarksViewModels { get; set; }

    }
    public class SemesterMarksViewModel
    {
        public int Mark { get; set; }
        public int Semester { get; set; } // e.g., 1

        public int Subject { get; set; } //e.g., Economics/Statistics
    }
}
