using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Common;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services;

namespace UniversityPortal.Controllers
{
    public class UniversityStaffController : Controller
    {
        private readonly IUniversityStaffService _universityStaffService;

        public UniversityStaffController(IUniversityStaffService universityStaffService)
        {
            _universityStaffService = universityStaffService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(UniversityStaffViewModel model)
        {
            await _universityStaffService.Save(model);
            return View();
        }
    }
}
