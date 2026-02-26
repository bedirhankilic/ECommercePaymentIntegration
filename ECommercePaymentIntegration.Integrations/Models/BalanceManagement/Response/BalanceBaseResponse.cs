#nullable disable
namespace ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response
{
    public class BalanceBaseResponse<T> where T : class
    {
        public bool? Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public string Message { get; set; }
    }
}
