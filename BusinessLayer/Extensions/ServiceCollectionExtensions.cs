using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DatabaseLayer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBL(this IServiceCollection services, 
            string connection)
        {
            services.AddScoped<IPartyServices, PartyService>();
            services.AddDL(connection);

            return services;
        }
    }
}
