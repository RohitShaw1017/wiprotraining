using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureLogin.Models;
using System.Diagnostics;

namespace SecureLogin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            ViewBag.Message = "Welcome, Admin! You have access to the Admin Dashboard.";
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult UserProfile()
        {
            ViewBag.Message = "Welcome, User! Here is your profile information.";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
