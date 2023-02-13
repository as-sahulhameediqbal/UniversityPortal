using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class StudentSemesterViewModel
    {
        public int SemesterYear { get; set; }
        public int Semester { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ExamPublishedDate { get; set; }      
        public bool IsPaid { get; set; }
        public bool IsResult { get; set; }

    }
}
