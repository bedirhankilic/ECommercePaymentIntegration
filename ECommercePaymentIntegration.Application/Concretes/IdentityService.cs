using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Domain.Settings;
using ECommercePaymentIntegration.Domain.Validators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SharedEx = ECommercePaymentIntegration.Shared.Exceptions;

namespace ECommercePaymentIntegration.Application.Concretes
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtConfig _options;
        public IdentityService(IOptions<JwtConfig> config) => _options = config.Value;
        public LoginResponse Login(LoginRequest req)
        {
            req.ValidateLoginRequest();

            if (string.Compare(_options.UserAppKey, req.AppKey) != 0 || string.Compare(_options.UserAppSecret, req.SecretKey) != 0)
            {
                throw new SharedEx.ApplicationException("Invalid credentials", "InvalidCredentials");
            }

            return new LoginResponse()
            {
                access_token = GenerateToken(),
                type = "Bearer"
            };
        }

        private string GenerateToken()
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, _options.AppKey),
                    new Claim("appKey", _options.AppKey)
             };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.AppSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
