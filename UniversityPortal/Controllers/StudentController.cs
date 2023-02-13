using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services;

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
            var genders = await _studentService.GetAllGender();
            var response = new StudentViewModel();
            response.IsActive = true;
            response.JoiningDate = DateTimeOffset.Now;
            response.Genders = genders;            
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
            string role = _studentService.GetRole();
            var response = await _studentService.Get(id);
            response.Role = role;
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var genders = await _studentService.GetAllGender();
            var response = await _studentService.Get(id);
            response.Genders = genders;
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

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            string role = _studentService.GetRole();                        
            var response = await _studentService.GetStudentProfile();
            response.Role = role;
            return View("View", response);
        }

        public async Task<IActionResult> Certificate()
        {
            var response = await _studentService.GetStudentProfile();
            return View("Certificate", response);
        }

        
        [HttpGet]
        public async Task<IActionResult> CourseComplete(Guid id)
        {
            await _studentService.UpdateIsComplete(id);
            return RedirectToAction("Index", "Student");
        }
    }
}
