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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{baseUri}api/Booking/GetBookingById/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorMsg);
                }

                var json = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Booking>(json);

                if (booking == null)
                {
                    return NotFound();
                }

                // Map Booking to EditBooking DTO
                var editBooking = new EditBooking
                {
                    Id = booking.Id,
                    TimeBooked = booking.TimeBooked,
                    CustomerCount = booking.CustomerCount,
                    TableId = booking.TableId
                };

                return View(editBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBooking booking)
        {
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            try
            {
                var json = JsonConvert.SerializeObject(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{baseUri}api/Booking/UpdateBooking/{booking.Id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error updating item");
                    return View(booking);
                }

                return RedirectToAction("ManageBookings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating item.");
                return View(booking);
            }
        }
    }
}
