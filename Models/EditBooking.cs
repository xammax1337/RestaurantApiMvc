using System.ComponentModel.DataAnnotations;

namespace RestaurantApiMvc.Models
{
    public class EditBooking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "TimeBooked is required")]
        public DateTime TimeBooked { get; set; }

        [Required(ErrorMessage = "Customer count is required")]
        [Range(1, 20, ErrorMessage = "Customer count must be between 1 and 20")]
        public int CustomerCount { get; set; }

        [Required(ErrorMessage = "Table ID is required")]
        public int TableId { get; set; }
    }
}
