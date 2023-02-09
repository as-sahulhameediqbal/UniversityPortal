namespace UniversityPortal.Entity
{
    public class Student : EntityBase
    {
        public string Name { get; set; } = null!;
        public string RollNumber { get; set; } = null!;
        public string Email { get; set; } = null!;       
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
