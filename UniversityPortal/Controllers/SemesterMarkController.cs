using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Faculty)]
    public class SemesterMarkController : Controller
    {
        private readonly ISemesterMarkService _semesterMarkService;

        public SemesterMarkController(ISemesterMarkService semesterMarkService)
        {
            _semesterMarkService = semesterMarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _semesterMarkService.GetSemesterAll();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> View(int sem, int year)
        {
            var response = await _semesterMarkService.GetSemesterStudentAll(sem, year);
            return View(response);
        }


        [HttpGet]
        public async Task<IActionResult> Mark(Guid id, int sem, int year)
        {
            var response = await _semesterMarkService.GetSemesterStudent(id, sem, year);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Publish(int sem, int year)
        {
            await _semesterMarkService.PublishMarkResult(sem, year);
            return RedirectToAction("Index", "SemesterMark");
        }
    }
}
