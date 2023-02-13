using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class SemesterStudentViewModel
    {
        public int Semester { get; set; }
        public int SemesterYear { get; set; }
        public bool IsPublish { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset PublishDate { get; set; }
        public bool IsResult { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ResultDate { get; set; }
        public bool IsPublishResult { get; set; }

        public IEnumerable<StudentModel> Students { get; set; } = null!;
    }

    public class StudentModel
    {
        public Guid StudentId { get; set; }
        public int Semester { get; set; }
        public int SemesterYear { get; set; }
        public string StudentName { get; set; } = null!;
        public string RollNumber { get; set; } = null!;
        public string Program { get; set; } = null!;
        public string Department { get; set; } = null!;

        public IEnumerable<StudentExamModel> CurrentPapers { get; set; } = null!;
        public IEnumerable<StudentExamModel> ArrearPapers { get; set; } = null!;
    }
}
