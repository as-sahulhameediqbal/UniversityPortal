using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [Authorize(Roles = UserRoles.PortalAdmin)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _universityService.GetAll();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = new UniversityViewModel();
            response.IsActive = true;
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UniversityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _universityService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "University");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _universityService.Get(id);            
            return View(response);
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var response = await _universityService.Get(id);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UniversityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _universityService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "University");
        }
    }
}
