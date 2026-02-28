namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class NotFoundException(string message, string errorCode = "NotFound", List<string>? errors = null, Exception? innerException = null) : ApplicationException(message, errorCode, errors, innerException)
    {
    }
}
