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
    }
}
