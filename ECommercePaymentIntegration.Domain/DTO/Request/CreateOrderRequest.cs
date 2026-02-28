namespace ECommercePaymentIntegration.Domain.DTO.Request
{
    public class CreateOrderRequest
    {
        public List<ProductOrderItem> Products { get; set; } = new();
    }

    public class ProductOrderItem
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }

}
