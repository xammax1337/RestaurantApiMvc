using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantApiMvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageBookings()
        {
            return View();
        }
        public IActionResult ManageMenu()
        {
            return View();
        }
        public IActionResult ManageCustomers()
        {
            return View();
        }
        public IActionResult ManageTables()
        {
            return View();
        }
    }
}
