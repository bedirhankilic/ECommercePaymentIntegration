using ECommercePaymentIntegration.Infrastructure.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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

            var redisConnectionString = configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
            var options = ConfigurationOptions.Parse(redisConnectionString);
            options.AbortOnConnectFail = false;
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(options));
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
