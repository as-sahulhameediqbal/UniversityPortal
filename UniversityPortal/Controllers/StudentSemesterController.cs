using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    public class StudentSemesterController : Controller
    {
        private IStudentExamService _studentExamService;

        public StudentSemesterController(IStudentExamService studentExamService)
        {
            _studentExamService = studentExamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _studentExamService.GetAllStudentSemester();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Result()
        {
            var response = await _studentExamService.GetAllStudentSemester(true);           
            return View("Index", response);
        }
    }
}
