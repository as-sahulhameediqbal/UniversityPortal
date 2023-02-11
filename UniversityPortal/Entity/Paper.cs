namespace UniversityPortal.Entity
{
    public class Paper : EntityBase
    {
        public string Name { get; set; } = null!;
        public int Semseter { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
