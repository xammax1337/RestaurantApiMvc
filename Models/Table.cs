using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestaurantApiMvc.Models
{
    public class Table
    {
        public int TableId { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("TableNumber")]
        public int TableNumber { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("Seats")]
        public int Seats { get; set; }
        public bool Available { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
