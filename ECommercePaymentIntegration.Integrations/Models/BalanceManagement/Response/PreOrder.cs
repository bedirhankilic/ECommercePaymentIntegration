using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;

namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response
{
    public class PreOrder
    {
        public OrderItem preOrder { get; set; }
        public BalanceItem updatedBalance { get; set; }
    }
}
