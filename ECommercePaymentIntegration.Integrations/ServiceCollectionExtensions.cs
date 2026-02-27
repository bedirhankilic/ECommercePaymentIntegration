using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Concretes;
using ECommercePaymentIntegration.Integrations.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Integrations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrations(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<BalanceManagementOptions>(config.GetSection("BalanceManagementOptions"));

            services.AddOptions<BalanceManagementOptions>()
                    .Validate(o => !string.IsNullOrWhiteSpace(o.BaseUrl), "BalanceManagementOptions:BaseUrl is required")
                    .ValidateOnStart();

            services.AddScoped<IBalanceIntegrationService, BalanceIntegrationService>();
        }
    }
}
