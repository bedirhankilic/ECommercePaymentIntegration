using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;

namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response
{
    public class CompleteResponse
    {
        public OrderItem order { get; set; }
        public BalanceItem updatedBalance { get; set; }
    }
}
