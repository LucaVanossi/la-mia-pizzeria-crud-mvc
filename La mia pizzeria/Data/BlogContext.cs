using La_mia_pizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace La_mia_pizzeria.Data
{
    public class BlogContext : DbContext
    {
        
        public DbSet<Pizza> Pizze { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=localhost;Database=La_mia_pizzeria;Integrated Security=True");
        }
    }

}
