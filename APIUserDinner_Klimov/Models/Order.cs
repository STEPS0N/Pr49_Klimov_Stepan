using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIUserDinner_Klimov.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public List<OrderDish> Dishes { get; set; }
    }
}
