using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.PortalAdmin)]
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
