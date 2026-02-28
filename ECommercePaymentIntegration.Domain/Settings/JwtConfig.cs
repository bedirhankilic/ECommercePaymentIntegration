namespace ECommercePaymentIntegration.Domain.Settings
{
    public class JwtConfig
    {
        public string AppKey { get; init; } = default!;
        public string AppSecret { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int ExpiryMinutes { get; init; } = 60;

        public string UserAppKey { get; set; }
        public string UserAppSecret { get; set; }
    }
}
