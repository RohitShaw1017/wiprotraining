using Microsoft.AspNetCore.Mvc;

namespace MvcRoutingApp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Orders(string username)
        {
            return Content($"Showing orders for user: {username}");
        }
    }
}
