using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentRegistration(StudentRegistrationViewModel studentRegistrationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(studentRegistrationViewModel);
            }
            var result = await _studentService.AddStudent(studentRegistrationViewModel);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(studentRegistrationViewModel);
            }
            return RedirectToAction("Index", "Student");
        }

        public IActionResult PaymentForSemesterFees(FeesViewModel feesViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(feesViewModel);
            }
            var result = await _studentService.AddFee(feesViewModel);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(feesViewModel);
            }
            return RedirectToAction("Index", "Student");
        }

        public IActionResult AddMarks(MarksViewModel marksViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(marksViewModel);
            }
            var result = await _studentService.AddMarks(marksViewModel);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(marksViewModel);
            }
            return RedirectToAction("Index", "Student");
        }

        //public ActionResult SemesterMarks(MarksViewModel marksViewModel) // partial view of AddMarks
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        ContactModel contact = new ContactModel();
        //        contact.ContactName = customer.ContactMode.ContactName;
        //        contact.ContactNo = customer.ContactMode.ContactNo;

        //        if (customer.Contacts == null)
        //        {
        //            customer.Contacts = new List<ContactModel>();
        //        }

        //        customer.Contacts.Add(contact);

        //        return PartialView("_Contact", customer);
        //    }
        //}
    }
}
