namespace ECommercePaymentIntegration.Shared.Exceptions
{
    public class DomainException(string message, string errorCode = "DomainError", List<string>? errors = null, Exception? innerException = null) : ApplicationException(message, errorCode, errors, innerException)
    {
    }
}
