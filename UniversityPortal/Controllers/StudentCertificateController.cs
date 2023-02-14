using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Interfaces.Services;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class StudentCertificateController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentCertificateController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _studentService.GetStudentCertificateAll();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyCertificate(Guid id, bool status)
        {
            await _studentService.VerifyStudentCertificate(id, status);
            return RedirectToAction("Index", "StudentCertificate");
        }
    }
}
