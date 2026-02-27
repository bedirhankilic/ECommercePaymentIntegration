namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class NotFoundException(string message, string errorCode = "NotFound", IDictionary<string, object>? metadata = null, Exception? innerException = null) : ApplicationException(message, errorCode, metadata, innerException)
    {
    }
}
