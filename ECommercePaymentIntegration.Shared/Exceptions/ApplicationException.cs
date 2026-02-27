namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        public IDictionary<string, object>? Metadata { get; }

        public ApplicationException(string message, string errorCode = "Business Error", IDictionary<string, object>? metadata = null, Exception? innerException = null) : base(message, innerException)
        {
            ErrorCode = errorCode;
            Metadata = metadata;
        }
    }
}
