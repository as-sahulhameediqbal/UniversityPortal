using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class StudentExamViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public Guid PaperId { get; set; }
        public string Subject { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTimeOffset ExamDate { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset PublishDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal Marks { get; set; }
        public bool IsPass { get; set; }
        public int Attempt { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset ResultDate { get; set; }
        public bool IsActive { get; set; }

    }
}
