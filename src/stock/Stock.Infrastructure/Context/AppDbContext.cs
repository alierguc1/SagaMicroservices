using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Context
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

            optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Server=localhost;Port=5432;Database=StockDB;Integrated Security=true;Pooling=true;");

        }


        public DbSet<OrderStock> OrderStocks { get; set; }
    }
}
