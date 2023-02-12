using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class CertificateViewModel
    {
        public string UniversityName { get; set; }
        public string Name { get; set; }
        public string ClassType { get; set; } // first second
        public string DegreeName { get; set; }
        public string Department { get; set; }
    }
}
