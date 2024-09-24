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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{baseUri}api/Table/GetTableById/{id}"); //Not a working endpoint atm.
                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorMsg);
                }

                var json = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<Table>(json);

                if (table == null)
                {
                    return NotFound();
                }

                return View(table);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }

            try
            {
                var json = JsonConvert.SerializeObject(table);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{baseUri}api/Table/UpdateTable/{table.TableId}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error updating item");
                    return View(table);
                }

                return RedirectToAction("ManageTables");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating item.");
                return View(table);
            }
        }
    }
}
