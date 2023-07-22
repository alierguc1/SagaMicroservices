using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DataAccess.IoC;

namespace Stock.Business.IoC
{
    public static class StockBusinessDependencies
    {
        public static IServiceCollection AddStockBusinessDependencies(
            this IServiceCollection services,
            string connectionString,
            string rabbitMqConnectionString)
        {
            //services.AddScoped<IOrderServices, OrderServices>();
            services.AddStockDataAccessDependencies(connectionString, rabbitMqConnectionString);
            return services;
        }
    }
}
