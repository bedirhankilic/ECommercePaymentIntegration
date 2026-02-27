namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class ExternalServiceException(string message, string errorCode = "External Error", IDictionary<string, object>? metadata = null, Exception? innerException = null) 
                : ApplicationException(message, errorCode, metadata, innerException)
    {

    }
}
