namespace UniversityPortal.Entity
{
    public class Student : EntityBase
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdmissionNumber { get; set; }
        public string DOB { get; set; }
        public string Program { get; set; }
        public bool Department { get; set; }
        public string Gender { get; set; }
        public string MailID { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string YearOfJoin { get; set; }
        public bool IsActive { get; set; }
    }

    public class Fees : EntityBase
    {
        public Guid Id { get; set; }
        public int FeeAmount { get; set; }
        public int Semester { get; set; }
    }

    public class Marks : EntityBase
    {
        public Guid Id { get; set; }
        public int AdmissionNumber { get; set; }
        public List<IndividualMarks> IndividualMarks { get; set; }

    }
    public class IndividualMarks
    {
        public int Mark { get; set; }
        public int Semester { get; set; } // e.g., 1

        public int Subject { get; set; } //e.g., Economics/Statistics
    }
}
