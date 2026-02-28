namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class ExternalServiceException(string message, string errorCode = "External Error", List<string>? errors = null, Exception? innerException = null) 
                : ApplicationException(message, errorCode, errors, innerException)
    {

    }
}
