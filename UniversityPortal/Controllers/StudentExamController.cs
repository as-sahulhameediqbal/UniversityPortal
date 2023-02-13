using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    public class StudentExamController : Controller
    {
        private IStudentExamService _studentExamService;
        private IStudentService _studentService;

        public StudentExamController(IStudentExamService studentExamService, IStudentService studentService)
        {
            _studentExamService= studentExamService;
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int sem, int year)
        {
            var response = await _studentExamService.GetStudentExam(sem, year);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewHallTicket(int sem, int year)
        {
            var response = await _studentService.DegreeCertificateExportToPDF();
            return response;
        }
    }
}
