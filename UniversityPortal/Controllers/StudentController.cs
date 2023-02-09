using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniversityPortal.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
