using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApiMvc.Models;
using System.Text;

namespace RestaurantApiMvc.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7103/";
        public AdminBookingController(HttpClient client)
        {
            _client = client;
        }

        public async Task <IActionResult> ManageBookings()
        {
            ViewData["Title"] = "AdminBooking";

            var response = await _client.GetAsync($"{baseUri}api/Booking/GetAllBookings");
            var json = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{baseUri}api/Booking/DeleteBooking/{id}");

            return RedirectToAction("ManageBookings");
        }

        public IActionResult AddBooking()
        {
            ViewData["Title"] = "Create booking";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddBooking booking)
        {
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUri}api/Booking/CreateBooking", content);

            return RedirectToAction("ManageBookings");
        }
    }
}
