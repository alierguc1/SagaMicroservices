using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.IoC
{
    public static class OrderDataAccessDependencies
    {
        public static IServiceCollection AddOrderDataAccessDependencies(this IServiceCollection services,string connectionString)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Data Source=DESKTOP-SFOU3KL;Initial Catalog=OrderDB; User Id=sa; Password=123; Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            });

            return services;
        }
    }
}
