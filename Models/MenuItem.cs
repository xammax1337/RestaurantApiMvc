using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestaurantApiMvc.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [DisplayName("Price")]
        public int Price { get; set; }
        public bool Available { get; set; }
    }
}
