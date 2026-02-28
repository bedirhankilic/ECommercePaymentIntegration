using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
namespace ECommercePaymentIntegration.Shared.Middlewares
{
    public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)

    {
        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.TraceIdentifier;

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next(context);
            }
        }
    }
}
