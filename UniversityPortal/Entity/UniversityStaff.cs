namespace UniversityPortal.Entity
{
    public class UniversityStaff : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid UniversityId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
