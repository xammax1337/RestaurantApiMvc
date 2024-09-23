using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;
using System.Diagnostics;

namespace RestaurantApiMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Menu";

            var response = await _client.GetAsync($"{baseUri}api/Menu/GetAllMenuItems");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
