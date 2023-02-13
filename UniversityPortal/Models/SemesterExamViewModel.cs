using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class SemesterExamViewModel
    {
        public int SemesterYear { get; set; }
        public int Semester { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ExamPublishedDate { get; set; }
        public bool IsPublish { get; set; }
    }
}
