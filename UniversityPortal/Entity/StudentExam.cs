namespace UniversityPortal.Entity
{
    public class StudentExam : EntityBase
    {
        public Guid StudentId { get; set; }
        public Guid PaperId { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public bool IsPublish { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal Marks { get; set; }
        public bool IsPass { get; set; }
        public int Attempt { get; set; }
        public bool IsResult { get; set; }
        public DateTimeOffset ResultDate { get; set; }
        public bool IsActive { get; set; }

    }
}
