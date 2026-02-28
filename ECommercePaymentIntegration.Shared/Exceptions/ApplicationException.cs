namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        public List<string>? Errors { get; }

        public ApplicationException(string message, string errorCode = "Business Error", List<string>? errors = null, Exception? innerException = null) : base(message, innerException)
        {
            ErrorCode = errorCode;
            Errors = errors;
        }
    }
}
