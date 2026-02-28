namespace ECommercePaymentIntegration.Shared.DTO
{
    public sealed class ProblemDetail
    {
        public int status { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public List<string>? errors { get; set; }
        public string code { get; set; }
        public string correlationId { get; set; }
    }
}
