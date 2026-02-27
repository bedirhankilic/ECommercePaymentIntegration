using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Concretes;
using ECommercePaymentIntegration.Integrations.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Net;

namespace ECommercePaymentIntegration.Integrations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<BalanceManagementOptions>(config.GetSection("BalanceManagementOptions"));

            services.AddOptions<BalanceManagementOptions>()
                    .Validate(o => !string.IsNullOrWhiteSpace(o.BaseUrl), "BalanceManagementOptions:BaseUrl is required")
                    .ValidateOnStart();

            services.AddHttpClient<IBalanceIntegrationService, BalanceIntegrationService>((sp, client) =>
            {
                var opt = sp.GetRequiredService<IOptions<BalanceManagementOptions>>().Value;

                client.BaseAddress = new Uri(opt.BaseUrl);
            }).AddPolicyHandler((sp, request) =>
            {
                var opt = sp.GetRequiredService<IOptions<BalanceManagementOptions>>().Value;

                // Timeout policy
                return Policy.TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromMilliseconds(opt.Timeoutms));
            }).AddPolicyHandler((sp, request) =>
            {
                var opt = sp.GetRequiredService<IOptions<BalanceManagementOptions>>().Value;

                return HttpPolicyExtensions
                        .HandleTransientHttpError() // 5xx + network
                        .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests) // 429
                        .Or<TimeoutRejectedException>()
                        .WaitAndRetryAsync(
                            opt.RetryCount,
                            attempt => TimeSpan.FromMilliseconds(200 * Math.Pow(2, attempt))
                        );
            })
            .AddPolicyHandler((sp, request) =>
            {
                var opt = sp.GetRequiredService<IOptions<BalanceManagementOptions>>().Value;

                return HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .Or<TimeoutRejectedException>()
                        .CircuitBreakerAsync(opt.BreakAfterFailures, TimeSpan.FromSeconds(opt.BreakDurationSeconds));
            });


            //DI
            services.AddScoped<IBalanceIntegrationService, BalanceIntegrationService>();


            return services;
        }
    }
}
