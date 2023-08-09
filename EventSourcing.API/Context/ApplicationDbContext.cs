using EventSourcing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Product> products { get; set; }
    }
}
