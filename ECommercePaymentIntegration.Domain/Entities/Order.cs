using ECommercePaymentIntegration.Domain.Enum;

namespace ECommercePaymentIntegration.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; }
        public string ExternalOrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }


        public virtual IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
