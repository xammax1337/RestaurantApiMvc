using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestaurantApiMvc.Models
{
    public class AddBooking
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Seats required must be at least 1.")]
        public int SeatsRequired { get; set; }

    }
}
