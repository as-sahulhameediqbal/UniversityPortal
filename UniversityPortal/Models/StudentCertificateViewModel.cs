using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class StudentCertificateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string AdmissionNumber { get; set; } = null!;
        public string RollNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Program { get; set; } = null!;
        public string Department { get; set; } = null!;

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset JoiningDate { get; set; }

        [Display(Name = "Course Completed Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset CompletedDate { get; set; }

        [Display(Name = "Applied University")]
        public string University { get; set; } = null!;

        public bool IsVerifyCertifiate { get; set; }
        public bool IsRejectCertifiate { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset VerifyDate { get; set; }
    }
}
