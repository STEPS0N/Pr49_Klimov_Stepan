using APIUserDinner_Klimov.Models;
using Microsoft.EntityFrameworkCore;

namespace APIUserDinner_Klimov.Context
{
    public class DishContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DishContext()
        {
            Database.EnsureCreated();
            Dishes.Load();
            Orders.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=Dinners;uid=root;pwd=;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
