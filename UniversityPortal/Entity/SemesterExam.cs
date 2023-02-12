namespace UniversityPortal.Entity
{
    public class SemesterExam : EntityBase
    {
        public Guid UniversityId { get; set; }
        public Guid PaperId { get; set; }
        public int Semester { get; set; }
        public int SemesterYear { get; set; }
        public DateTimeOffset ExamDate { get; set; }       
        public bool IsArrear { get; set; }
        public bool IsPublish { get; set; }
        public DateTimeOffset PublishDate { get; set; }      
        public bool IsActive { get; set; }
    }
}
