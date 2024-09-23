using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;
using System.Text;

namespace RestaurantApiMvc.Controllers
{
    public class AdminTableController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        public AdminTableController(HttpClient client)
        {
            _client = client;
        }
        public async Task <IActionResult> ManageTables()
        {
            var response = await _client.GetAsync($"{baseUri}api/Table/GetAllTables");
            var json = await response.Content.ReadAsStringAsync();
            var tables = JsonConvert.DeserializeObject<List<Table>>(json);

            return View(tables);
        }

        public IActionResult AddTable()
        {
            ViewData["Title"] = "New Table";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(AddTable table)
        {
            if(!ModelState.IsValid)
            {
                return View(table);
            }

            var json = JsonConvert.SerializeObject(table);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUri}api/Table/AddTable", content);

            return RedirectToAction("ManageTables");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{baseUri}api/Table/DeleteTable/{id}");

            return RedirectToAction("ManageTables");
        }
    }
}
