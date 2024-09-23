using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;

namespace RestaurantApiMvc.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        public MenuController(HttpClient client)
        {
            _client = client;
        }

        public async Task <IActionResult> Index()
        {
            ViewData["Title"] = "Menu";

            var response = await _client.GetAsync($"{baseUri}api/Menu/GetAllMenuItems");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
        }
    }
}
