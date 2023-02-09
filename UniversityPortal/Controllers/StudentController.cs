using Microsoft.AspNetCore.Mvc;

namespace UniversityPortal.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
