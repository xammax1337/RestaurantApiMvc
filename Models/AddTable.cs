using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestaurantApiMvc.Models
{
    public class AddTable
    {
        public int TableId { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("TableNumber")]
        public int TableNumber { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("Seats")]
        public int Seats { get; set; }
    }
}
