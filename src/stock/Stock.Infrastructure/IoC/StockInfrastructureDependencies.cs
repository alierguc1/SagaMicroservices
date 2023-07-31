using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Settings;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stock.Infrastructure.IoC
{
    public static class StockInfrastructureDependencies
    {
        public static IServiceCollection AddStockInfrastructureDependencies(
           this IServiceCollection services,
           string connectionString,
           string rabbitMqConnection)
        {

            //services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, conf) =>
                {
                    conf.Host(rabbitMqConnection);
                });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });


            return services;
        }
    }
}
