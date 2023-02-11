using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _studentService.GetAll();
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = new StudentViewModel();
            response.IsActive = true;
            response.JoiningDate = DateTimeOffset.Now;
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _studentService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var response = await _studentService.Get(id);
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _studentService.Get(id);
            return View(response);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _studentService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "Student");
        }


        //public async Task<IActionResult> PaymentForSemesterFees(FeesViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await _studentService.AddFee(model);
        //    if (!result.Success)
        //    {
        //        TempData["Error"] = result.Message;
        //        return View(model);
        //    }
        //    return RedirectToAction("Index", "Student");
        //}

        //public async Task<IActionResult> AddMarks(MarksViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await _studentService.AddMarks(model);
        //    if (!result.Success)
        //    {
        //        TempData["Error"] = result.Message;
        //        return View(model);
        //    }
        //    return RedirectToAction("Index", "Student");
        //}

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
