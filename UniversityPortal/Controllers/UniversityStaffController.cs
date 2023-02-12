using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class UniversityStaffController : Controller
    {
        private readonly IUniversityStaffService _universityStaffService;

        public UniversityStaffController(IUniversityStaffService universityStaffService)
        {
            _universityStaffService = universityStaffService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _universityStaffService.GetAll();
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _universityStaffService.GetAllRoles();
            var response = new UniversityStaffViewModel();            
            
            response.IsActive = true;
            response.Roles= roles;
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(UniversityStaffViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _universityStaffService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "UniversityStaff");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var response = await _universityStaffService.Get(id);
            return View(response);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var roles = await _universityStaffService.GetAllRoles();
            var response = await _universityStaffService.Get(id);
            response.Roles = roles;
            return View(response);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(UniversityStaffViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _universityStaffService.Save(model);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            return RedirectToAction("Index", "UniversityStaff");
        }
    }
}
