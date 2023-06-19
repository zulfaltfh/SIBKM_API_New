using API.ViewModels;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountsRepository repository;

        public AccountsController(AccountsRepository repository)
        {
            this.repository = repository;
        }

        /*
         * LOGIN
         */
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var results = await repository.Login(login);
            if (results.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", results.Data);
                return RedirectToAction("Index","Home");
            } else if (results.Code == 409)
            {
                ModelState.AddModelError(string.Empty, results.Message);
                return View();
            }
            return View();
        }

        /*
         * REGISTER
         */
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            var results = await repository.Register(register);
            if (results.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", results.Data);
                return RedirectToAction("Login", "Accounts");
            }
            else if (results.Code == 409)
            {
                ModelState.AddModelError(string.Empty, results.Message);
                return View();
            }
            return View();
        }
    }
}
