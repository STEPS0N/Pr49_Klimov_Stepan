using System.ComponentModel.DataAnnotations;

namespace APIUserDinner_Klimov.Models
{
    public class OrderDish
    {
        [Key]
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}
