using System.ComponentModel.DataAnnotations;
namespace UniversityPortal.Models
{
    public class StudentRegistrationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int AdmissionNumber { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        [Required]
        public int Program { get; set; }
        [Required]
        public int Department { get; set; }
        [Required]
        public string Gender { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string MailID { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime YearOfJoin { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
