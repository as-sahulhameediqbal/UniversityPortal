namespace UniversityPortal.Entity
{
    public class Payment : EntityBase
    {
        public string StudentName { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
