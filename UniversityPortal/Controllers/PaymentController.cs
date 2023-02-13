using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string type, int sem, int year)
        {
            PaymentViewModel model = new PaymentViewModel();
            if (!string.IsNullOrEmpty(type))
            {
                model.FeesType = type;
                model.Sem = sem;
                model.Year = year;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _paymentService.Save(model);
            if (string.IsNullOrEmpty(model.FeesType))
            {
                return RedirectToAction("ViewProfile", "Student");
            }
            else
            {
                return RedirectToAction("Index", "StudentSemester");
            }
        }
    }
}
