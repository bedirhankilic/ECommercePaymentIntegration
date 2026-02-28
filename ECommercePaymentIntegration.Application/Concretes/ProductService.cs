using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Infrastructure.Cache;
using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;
using Microsoft.Extensions.Logging;

namespace ECommercePaymentIntegration.Application.Concretes
{
    public class ProductService(ILogger<ProductService> _logger, IBalanceIntegrationService _balanceService, ICacheService _cache) : IProductService
    {
        public async Task<IEnumerable<ProductItemDto>> GetProductItemsAsync(CancellationToken cancellation)
        {

            var cacheProducts = await _cache.GetAsync<IEnumerable<ProductItemDto>>("product_items");
            if (cacheProducts is not null)
            {
                _logger.LogInformation("Product items retrieved from cache.");
                return cacheProducts;
            }

            BalanceBaseResponse<List<ProductItem>> response = await _balanceService.GetProducts(cancellation);

            if (response.Data is null)
            {
                _logger.LogError("Failed to retrieve product items: {Error}", response.Error);
                throw new ApplicationException($"Failed to retrieve product items: {response.Error}");
            }

            var products = response.Data.Select(item => new ProductItemDto
            {
                Id = item.id,
                Name = item.name,
                Description = item.description,
                Price = item.price,
                Currency = item.currency,
                Category = item.category,
                Stock = item.stock
            });

            await _cache.SetAsync("product_items", products, TimeSpan.FromMinutes(10));

            return products;
        }
    }
}
