namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items
{
    public class BalanceItem
    {
        public string userId { get; set; }

        public decimal totalBalance { get; set; }

        public decimal availableBalance { get; set; }

        public decimal blockedBalance { get; set; }

        public string currency { get; set; }
        public DateTime? lastUpdated { get; set; }
    }
}
