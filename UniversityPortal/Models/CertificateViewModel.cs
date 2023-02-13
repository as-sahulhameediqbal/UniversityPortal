using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class CertificateViewModel
    {
        public string UniversityName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ClassType { get; set; } = null!; // first second
        public string DegreeName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string DOB { get; set; }
        public string RollNo { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Month { get; set; } = null!;
        public string Year { get; set; } = null!;
    }
}
