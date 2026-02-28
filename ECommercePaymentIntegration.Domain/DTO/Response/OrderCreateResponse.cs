namespace ECommercePaymentIntegration.Domain.DTO.Response
{
    public class OrderCreateResponse
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
