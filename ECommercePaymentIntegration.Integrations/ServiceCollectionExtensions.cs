using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Integrations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrations(this IServiceCollection services)
        {
            services.AddScoped<IBalanceIntegrationService, BalanceIntegrationService>();
        }
    }
}
