namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items
{
    public class ProductItem
    {
        public string id { get; set; } 

        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public string currency { get; set; }

        public string category { get; set; } 

        public int stock { get; set; }
    }
}
