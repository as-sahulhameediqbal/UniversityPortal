namespace UniversityPortal.Entity
{
    public class StudentExam : EntityBase
    {
        public Guid UniversityId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SemesterExamId { get; set; }
        public Guid PaperId { get; set; }      
        public bool IsPaid { get; set; }
        public decimal Marks { get; set; }
        public bool IsPass { get; set; }
        public int Attempt { get; set; }
        public bool IsResult { get; set; }
        public DateTimeOffset ResultDate { get; set; }
        public bool IsActive { get; set; }

    }
}
