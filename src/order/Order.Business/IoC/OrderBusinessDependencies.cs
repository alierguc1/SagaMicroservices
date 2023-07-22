using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.DataAccess.IoC;
using Order.Business.Services.Interfaces;
using Order.Business.Services.Concrete;

namespace Order.Business.IoC
{
    public static class OrderBusinessDependencies
    {
        public static IServiceCollection AddOrderBusinessDependencies(
            this IServiceCollection services,
            string connectionString,
            string rabbitMqConnectionString)
        {
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddOrderDataAccessDependencies(connectionString, rabbitMqConnectionString);
            return services;
        }
    }
}
