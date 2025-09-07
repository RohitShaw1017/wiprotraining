using Microsoft.AspNetCore.Mvc;

namespace MvcRoutingApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index(string role)
        {
            if (role == "admin")
                return View("Admin");
            else
                return View("User");
        }
    }
}
