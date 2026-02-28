using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommercePaymentIntegration.Shared.Middlewares
{
    public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)

    {
        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.TraceIdentifier;
            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            })) ;
            await next(context);

        }
    }
}
