namespace ECommercePaymentIntegration.Integrations.Configurations
{
    public class BalanceManagementOptions
    {
        public string BaseUrl { get; set; }
        public int Timeoutms { get; set; } = 2000;

        public int RetryCount { get; init; } = 3;
        public int BreakAfterFailures { get; init; } = 5;
        public int BreakDurationSeconds { get; init; } = 30;
    }
}
