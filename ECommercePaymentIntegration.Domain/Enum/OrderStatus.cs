namespace ECommercePaymentIntegration.Domain.Enum
{
    public enum OrderStatus
    {
        Created = 0,    // sipariş oluşturuldu ve ödeme için rezervasyon yapıldı
        Completed = 1,   // payment complete edildi
        Failed = 2       // business/tech sebeplerle failed (opsiyonel)
    }
}
