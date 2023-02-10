using System.ComponentModel.DataAnnotations;

namespace UniversityPortal.Models
{
    public class UniversityStaffViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }       

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public Guid UniversityId { get; set; }

    }
}
