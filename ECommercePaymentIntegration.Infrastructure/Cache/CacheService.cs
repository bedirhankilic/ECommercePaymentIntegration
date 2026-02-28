using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ECommercePaymentIntegration.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly ILogger<CacheService> _logger;
        public CacheService(IConnectionMultiplexer connectionMultiplexer, ILogger<CacheService> logger)
        {
            _db = connectionMultiplexer.GetDatabase();
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);

            if (!value.HasValue)
                return default;


            return JsonConvert.DeserializeObject<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {

            var json = JsonConvert.SerializeObject(value);
            if (expiry is null)
                expiry = TimeSpan.FromDays(1);

            await _db.StringSetAsync(key, json, expiry.Value);
        }
    }
}
