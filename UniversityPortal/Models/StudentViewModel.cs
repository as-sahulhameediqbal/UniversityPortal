using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace UniversityPortal.Models
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Admission Number")]
        public string AdmissionNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string Program { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Gender { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        public string Address { get; set; } = null!;

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset JoiningDate { get; set; }

        [Required]
        [Display(Name = "Tution Fee")]
        [DataType(DataType.Currency)]
        public decimal TutionFee { get; set; }
        public bool IsActive { get; set; }

        public Guid UniversityId { get; set; }
        [Display(Name = "Amount Paid")]
        public bool IsPaid { get; set; } 
        public List<SelectListItem>? Genders { get; set; } = null;
    }
}
