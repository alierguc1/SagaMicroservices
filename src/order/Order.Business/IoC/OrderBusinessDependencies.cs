using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.DataAccess.IoC;

namespace Order.Business.IoC
{
    public static class OrderBusinessDependencies
    {
        public static IServiceCollection AddOrderBusinessDependencies(
            this IServiceCollection services,
            string connectionString,
            string rabbitMqConnectionString)
        {
            services.AddOrderDataAccessDependencies(connectionString, rabbitMqConnectionString);
            return services;
        }
    }
}
