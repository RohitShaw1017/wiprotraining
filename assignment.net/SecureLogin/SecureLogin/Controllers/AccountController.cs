using Microsoft.AspNetCore.Mvc;

namespace SecureLogin.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            return RedirectToPage("/Account/Logout", new { area = "Identity" });
        }
    }
}
