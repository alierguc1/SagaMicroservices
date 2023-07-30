using Microsoft.EntityFrameworkCore;
using ServiceB.API.Models;

namespace ServiceB.API.ApplicationDBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Server=localhost;Port=5432;Database=ServiceB_DB;Integrated Security=true;Pooling=true;");

        }


        public DbSet<Product> Products { get; set; }
    }
}
