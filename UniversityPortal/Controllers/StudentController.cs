using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services;
using Rotativa.AspNetCore;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUniversityService _universityService;

        public StudentController(IStudentService studentService, IUniversityService universityService)
        {
            _studentService = studentService;
            _universityService = universityService;
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
            var response = await _studentService.Get(id);
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

        public IActionResult DegreeCertificateExportToPDF()
        {
            CertificateViewModel certificateViewModel = new CertificateViewModel();
            certificateViewModel = new CertificateViewModel();
            certificateViewModel.Name = "Kirubakaran";
            certificateViewModel.ClassType = "FIRST CLASS with Distinction";
            certificateViewModel.DegreeName = "MCA";
            certificateViewModel.Department = "Computer Applications";
            certificateViewModel.UniversityName = "Bharathiar";

            // for export "Rotativa" used (wkhtmltopdf.exe)
            if (certificateViewModel != null)
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                var report = new ViewAsPdf("DegreeCertificateExportToPDF", certificateViewModel)
                {
                    PageMargins = { Left = 0, Bottom = 5, Right = 10, Top = 5 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                };

                return report;
            }
            return NoContent();
        }

        public IActionResult ProvisionalCertificateExportToPDF()
        {
            var studresponse = await _studentService.GetStudentProfile();
            var univresponse = await _universityService.Get(response.UniversityId);

            CertificateViewModel certificateViewModel = new CertificateViewModel();
            certificateViewModel = new CertificateViewModel();
            certificateViewModel.Name = studresponse.Name;
            certificateViewModel.UniversityName = univresponse.Name;

            // for export "Rotativa" used (wkhtmltopdf.exe)
            if (certificateViewModel != null)
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                var report = new ViewAsPdf("DegreeCertificateExportToPDF", certificateViewModel)
                {
                    PageMargins = { Left = 0, Bottom = 5, Right = 10, Top = 5 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                };

                return report;
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var response = await _studentService.GetStudentProfile();
            return View("View", response);
        }
    }
}
