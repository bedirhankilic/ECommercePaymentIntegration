using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Domain.DTO.Response;

namespace ECommercePaymentIntegration.Application.Abstraction
{
    public interface IOrdersService
    {
        Task<OrderCreateResponse> CreateOrderAsync(CreateOrderRequest req , CancellationToken ct);

        Task<OrderCreateResponse> CompleteOrderAsync(string orderId ,CancellationToken ct);
    }
}
