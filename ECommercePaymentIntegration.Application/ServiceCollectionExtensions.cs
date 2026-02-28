using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Application.Concretes;
using ECommercePaymentIntegration.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services, IConfiguration config)
        {

            services.AddIntegrations(config);


            //redis sonrası kontrol et
            services.AddTransient<IProductService, ProductService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }
    }
}
