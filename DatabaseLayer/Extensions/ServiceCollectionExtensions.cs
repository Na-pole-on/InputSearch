using DatabaseLayer.Database;
using DatabaseLayer.Interfaces;
using DatabaseLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDL(this IServiceCollection services,
            string connection)
        {
            services.AddDbContext<AppDatabase>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddScoped<IUnitOfWork, AppUnitOfWork>();

            return services;
        }
    }
}
