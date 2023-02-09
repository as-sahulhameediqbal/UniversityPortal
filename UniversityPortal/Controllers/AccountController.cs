using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;

namespace UniversityPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var result = await _accountService.Login(loginViewModel);
            if (!result)
            {
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }            
            return RedirectToAction("Index", "Home");
        }

    }
}
