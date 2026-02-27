namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class DomainException(string message, string errorCode = "DomainError", IDictionary<string, object>? metadata = null, Exception? innerException = null) : ApplicationException(message, errorCode, metadata, innerException)
    {
    }
}
