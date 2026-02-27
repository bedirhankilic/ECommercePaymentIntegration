using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection  AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("DbConnection")));


            return services;
        }
    }
}
