namespace UniversityPortal.Entity
{
    public class University : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
