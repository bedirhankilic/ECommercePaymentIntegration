using ECommercePaymentIntegration.Domain.DTO.Response;

namespace ECommercePaymentIntegration.Application.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetProductItemsAsync(CancellationToken cancellation);
    }
}
