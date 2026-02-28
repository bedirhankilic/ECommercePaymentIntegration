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


        public static void ValidateCreateOrderRequest(this CreateOrderRequest req)
        {
            if (req.Products == null || !req.Products.Any())
            {
                throw new DomainException("At least one product is required");
            }
            foreach (var item in req.Products)
            {
                if (string.IsNullOrEmpty(item.ProductId))
                {
                    throw new DomainException("ProductId is required for each product");
                }
                if (item.Quantity <= 0)
                {
                    throw new DomainException("Quantity must be greater than zero for each product");
                }
            }

            req.Products.GroupBy(p => p.ProductId)
                .Where(g => g.Count() > 1)
                .ToList()
                .ForEach(g =>
                {
                    throw new DomainException($"Duplicate ProductId found: {g.Key}");
                });

        }
    }
}
