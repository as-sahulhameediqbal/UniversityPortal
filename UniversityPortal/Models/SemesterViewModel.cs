using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class SemesterViewModel
    {
        public int SemesterYear { get; set; }
        public int Semester { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ResultDate { get; set; }
        public bool IsResult { get; set; }
    }
}
