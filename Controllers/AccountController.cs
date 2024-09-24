using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;
using static RestaurantApiMvc.Models.Login;

namespace RestaurantApiMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        public AccountController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // Handle login logic here
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                // Handle registration logic here (save user to DB, etc.)
                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
