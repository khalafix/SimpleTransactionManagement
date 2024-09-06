using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    public class Users1Controller : Controller
    {
        private readonly SignInManager<User> _signInManager;
        public Users1Controller(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LgoinAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // login
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "نام کاربری یا کلمه عبور نادرست است!");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {

            return View();
        }
    }
}
