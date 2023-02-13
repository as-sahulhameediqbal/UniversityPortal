using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class SemesterExamPaperViewModel
    {
        public IEnumerable<SemesterExamModel> Semester { get; set; }
        public IEnumerable<SemesterExamModel> Arrear { get; set; }
        public bool IsPublish { get; set; }
        public int Sem { get; set; }
        public int Year { get; set; }

    }

    public class SemesterExamModel
    {
        public int Semester { get; set; }
        public int BaseSemester { get; set; }
        public string Subject { get; set; }

        public int SemesterYear { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset ExamDate { get; set; }
    }
}
