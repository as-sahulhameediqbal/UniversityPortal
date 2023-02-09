using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = new UniversityListViewModel();
            response.University = null;
            return View(response);
        }


    }
}
