using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services;

namespace UniversityPortal.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IStudentService _studentService;

        public PaymentController(IPaymentService paymentService, IStudentService studentService)
        {
            _paymentService = paymentService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string type)
        {            
            PaymentViewModel model =new PaymentViewModel();
            model.FeesType = type;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _paymentService.Save(model);
            //if (!result.Success)
            //{
            //    TempData["Error"] = result.Message;
            //    return View(model);
            //}
            return RedirectToAction("ViewProfile", "Student");
        }
    }
}
