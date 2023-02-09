namespace UniversityPortal.Entity
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
