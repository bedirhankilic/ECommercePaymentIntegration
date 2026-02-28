using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Application.Concretes
{
    public class ProductService(ILogger<ProductService> _logger, IBalanceIntegrationService _balanceService) : IProductService
    {
        public async Task<IEnumerable<ProductItemDto>> GetProductItemsAsync(CancellationToken cancellation)
        {
            BalanceBaseResponse<List<ProductItem>> response = await _balanceService.GetProducts(cancellation);

            if (response.Data is null)
            {
                _logger.LogError("Failed to retrieve product items: {Error}", response.Error);
                throw new ApplicationException($"Failed to retrieve product items: {response.Error}");
            }

            return response.Data.Select(item => new ProductItemDto
            {
                Id = item.id,
                Name = item.name,
                Description = item.description,
                Price = item.price,
                Currency = item.currency,
                Category = item.category,
                Stock = item.stock
            });
        }
    }
}
