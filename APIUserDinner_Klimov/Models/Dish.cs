using System.ComponentModel.DataAnnotations;

namespace APIUserDinner_Klimov.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        public string Category { get; set; }
        public string NameDish { get; set; }
        public int Price { get; set; }
        public string Icon { get; set; }
        public string Version { get; set; }
    }
}
