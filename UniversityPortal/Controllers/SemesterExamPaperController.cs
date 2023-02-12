using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.COE)]

    public class SemesterExamPaperController : Controller
    {
        private readonly ISemesterExamService _semesterExamService;

        public SemesterExamPaperController(ISemesterExamService semesterExamService)
        {
            _semesterExamService = semesterExamService;

        }
        [HttpGet]
        public async Task<IActionResult> Index(int sem, int year)
        {
            var response = await _semesterExamService.GetAllSemesterExamPaper(sem, year);
            response.Sem = sem;
            response.Year = year;
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var response = new SemesterExamPaperViewModel();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Publish(int sem, int year)
        {
            await _semesterExamService.PublishExam(sem, year);
            return RedirectToAction("Index", "SemesterExam");
        }

    }
}
