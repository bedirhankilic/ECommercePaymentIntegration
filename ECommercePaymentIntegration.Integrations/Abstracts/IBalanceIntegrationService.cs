using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Request;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;

namespace ECommercePaymentIntegration.Integrations.Abstracts
{
    public interface IBalanceIntegrationService
    {
        Task<BalanceBaseResponse<BalanceItem>> GetBalance(CancellationToken cancellation = default);
        Task<BalanceBaseResponse<List<ProductItem>>> GetProducts(CancellationToken cancellation = default);
        Task<BalanceBaseResponse<PreOrderResponse>> PreOrder(PreOrder request, CancellationToken cancellation = default);
        Task<BalanceBaseResponse<CompleteResponse>> CompleteOrder(CompleteRequest request, CancellationToken cancellation = default);
        Task<BalanceBaseResponse<CancelResponse>> CancelOrder(CancelRequest request, CancellationToken cancellation = default);
    }
}
