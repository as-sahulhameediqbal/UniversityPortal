using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class StudentExamViewModel
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public IEnumerable<StudentExamModel> CurrentPapers { get; set; } = null!;
        public IEnumerable<StudentExamModel> ArrearPapers { get; set; } = null!;

        public int Semester { get; set; }
        public int SemesterYear { get; set; }
        public bool IsPublish { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset PublishDate { get; set; }
        public bool IsResult { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ResultDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }      

    }

    public class StudentExamModel
    {
        public Guid StudentExamId { get; set; }
        public string Subject { get; set; }            
        public decimal Marks { get; set; }
        public bool IsPass { get; set; }
        public int Attempt { get; set; }
        public int BaseSemester { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ExamDate { get; set; }
        public bool IsResult { get; set; }
    }

    public class StudentMark
    {
        public Guid Id { get; set; }
        [JsonProperty("Marks")]
        public string Marks { get; set; }
    }
}
