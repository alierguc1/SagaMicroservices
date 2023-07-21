using Microsoft.EntityFrameworkCore;
using Order.DataAccess.DataMapper;
using Order.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext() : base()
        {
        }
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SFOU3KL;Initial Catalog=OrderDB; User Id=sa; Password=123; Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersTypeConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
