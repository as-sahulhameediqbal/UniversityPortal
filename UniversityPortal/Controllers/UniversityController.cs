using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;

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
        public async Task<IActionResult> Index()
        {
            var response = await _universityService.GetAll();            
            return View(response);
        }


    }
}
