using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.DataAccess.Consumers;
using Order.DataAccess.Context;
using Order.DataAccess.Repository.Concrete;
using Order.DataAccess.Repository.Interface;
using Shared.Settings;
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
                x.AddConsumer<PaymentCompletedEventConsumer>();
                x.AddConsumer<PaymentFailEventConsumer>();
                x.UsingRabbitMq((context, conf) =>
                {
                    conf.Host(rabbitMqConnection);
                    conf.ReceiveEndpoint(RabbitMQSettingsConst.ORDER_PAYMENT_COMPLETED_QUEUE_NAME, e =>
                    {
                        e.ConfigureConsumer<PaymentCompletedEventConsumer>(context);
                    });
                    conf.ReceiveEndpoint(RabbitMQSettingsConst.ORDER_PAYMENT_FAILED_EVENT_QUEUE_NAME, e =>
                    {
                        e.ConfigureConsumer<PaymentFailEventConsumer>(context);
                    });
                    
                });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

           
            return services;
        }
    }
}
