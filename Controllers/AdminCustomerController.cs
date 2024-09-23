using Microsoft.AspNetCore.Mvc;

namespace RestaurantApiMvc.Controllers
{
    public class AdminCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
