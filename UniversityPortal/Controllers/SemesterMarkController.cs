using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Faculty)]
    public class SemesterMarkController : Controller
    {
        private readonly ISemesterMarkService _semesterMarkService;
        private readonly IStudentExamService _studentExamService;

        public SemesterMarkController(ISemesterMarkService semesterMarkService, IStudentExamService studentExamService)
        {
            _semesterMarkService = semesterMarkService;
            _studentExamService = studentExamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _semesterMarkService.GetSemesterAll();
            return View("Index", response);
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

        [HttpGet]
        public async Task<IActionResult> SaveMark(string model)
        {
            await _studentExamService.UpdateStuentMark(model);
            return RedirectToAction("Index", "SemesterMark");
        }
    }
}
