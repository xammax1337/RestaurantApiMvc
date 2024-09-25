using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantApiMvc.Controllers
{
    [Authorize]
    public class AdminCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
