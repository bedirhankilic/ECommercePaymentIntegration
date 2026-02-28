using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Infrastructure.Cache
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task<T?> GetAsync<T>(string key);
    }
}
