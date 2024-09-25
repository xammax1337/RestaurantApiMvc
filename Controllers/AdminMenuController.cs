using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;
using System.Net.Http.Headers;
using System.Text;

namespace RestaurantApiMvc.Controllers
{
    [Authorize]
    public class AdminMenuController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        public AdminMenuController(HttpClient client)
        {
            _client = client;
        }

        [Authorize]
        public async Task <IActionResult> ManageMenu()
        {
            ViewData["Title"] = "AdminMenu";

            var token = HttpContext.Request.Cookies["jwtToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUri}api/Menu/GetAllMenuItems");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
        }
        
        public IActionResult AddMenuItem() 
        {
            ViewData["Title"] = "New MenuItem";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(MenuItem item)
        {
            if(!ModelState.IsValid)
            {
                return View(item);
            }
            
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUri}api/Menu/AddMenuItem", content);

            return RedirectToAction("ManageMenu");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{baseUri}api/Menu/GetMenuItemById/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorMsg);
                }

                var json = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<MenuItem>(json);

                if (item == null)
                {
                    return NotFound();
                }

                return View(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{baseUri}api/Menu/UpdateMenuItem/{item.Id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error updating item");
                    return View(item);
                }

                return RedirectToAction("ManageMenu");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating item.");
                return View(item);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{baseUri}api/Menu/DeleteMenuItem/{id}");

            return RedirectToAction("ManageMenu");
        }
    }
}
