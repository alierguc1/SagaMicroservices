using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.DataAccess.Context;
using Order.DataAccess.Repository.Concrete;
using Order.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.IoC
{
    public static class OrderDataAccessDependencies
    {
        public static IServiceCollection AddOrderDataAccessDependencies(
            this IServiceCollection services,
            string connectionString,
            string rabbitMqConnection)
        {

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, conf) =>
                {
                    conf.Host(rabbitMqConnection);
                });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Data Source=DESKTOP-SFOU3KL;Initial Catalog=OrderDB; User Id=sa; Password=123; Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            });

           
            return services;
        }
    }
}
