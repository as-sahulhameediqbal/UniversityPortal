using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.COE)]
    public class SemesterExamController : Controller
    {
        private readonly ISemesterExamService _semesterExamService;

        public SemesterExamController(ISemesterExamService semesterExamService)
        {
            _semesterExamService = semesterExamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _semesterExamService.GetAllSemesterExam();
            return View(response);
        }

    }
}
