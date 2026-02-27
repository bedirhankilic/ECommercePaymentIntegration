using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items
{
    public class OrderItem
    {
        public string orderId { get; set; }
        public decimal amount { get; set; }
        public DateTime timestamp { get; set; }
        public string status { get; set; }
        public DateTime? completedAt { get; set; }
        public DateTime? cancelledAt { get; set; }
    }
}
