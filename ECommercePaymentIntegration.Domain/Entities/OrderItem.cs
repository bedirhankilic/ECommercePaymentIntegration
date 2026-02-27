namespace ECommercePaymentIntegration.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal PriceTotal { get; set; } = 0;

        public string Currency { get; set; }

        public string Category { get; set; }


        public virtual Order Order { get; set; }
    }
}
