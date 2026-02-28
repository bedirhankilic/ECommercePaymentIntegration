namespace ECommercePaymentIntegration.Domain.DTO.Response
{
    public class LoginResponse
    {
        public string access_token { get; set; }
        public string type { get; set; } = "Bearer";


    }
}
