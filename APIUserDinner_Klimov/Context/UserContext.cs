using APIUserDinner_Klimov.Models;
using Microsoft.EntityFrameworkCore;

namespace APIUserDinner_Klimov.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public UserContext()
        {
            Database.EnsureCreated();
            User.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=Users;uid=root;pwd=;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
