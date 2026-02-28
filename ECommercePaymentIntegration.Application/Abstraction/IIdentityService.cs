using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Domain.DTO.Response;

namespace ECommercePaymentIntegration.Application.Abstraction
{
    public interface IIdentityService
    {
        LoginResponse Login(LoginRequest req);
    }
}
