using APIUserDinner_Klimov.Models;
using Microsoft.EntityFrameworkCore;

namespace APIUserDinner_Klimov.Context
{
    public class DishContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> orderDishes { get; set; }

        public DishContext()
        {
            Database.EnsureCreated();
            User.Load();
            Dishes.Load();
            Orders.Load();
            orderDishes.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=Dinners;uid=root;pwd=;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
