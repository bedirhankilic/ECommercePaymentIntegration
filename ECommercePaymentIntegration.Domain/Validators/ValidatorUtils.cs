using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Shared.Exceptions;

namespace ECommercePaymentIntegration.Domain.Validators
{
    public static class ValidatorUtils
    {
        public static void ValidateLoginRequest(this LoginRequest lr)
        {
            if (string.IsNullOrEmpty(lr.AppKey))
            {
                throw new DomainException("AppKey is required");
            }
            if (string.IsNullOrEmpty(lr.SecretKey))
            {
                throw new DomainException("SecretKey is required");
            }
        }
    }
}
