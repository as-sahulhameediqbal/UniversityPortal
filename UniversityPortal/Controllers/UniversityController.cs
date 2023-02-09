using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        [Authorize(Roles = UserRoles.PortalAdmin)]
        public async Task<IActionResult> Index()
        {
            var response = new UniversityListViewModel();
            response.University = null;
            return View(response);
        }


    }
}
