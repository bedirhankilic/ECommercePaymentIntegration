using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ECommercePaymentIntegration.Shared.Middlewares
{
    public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> _logger)
    {
        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            var method = context.Request.Method;
            var path = context.Request.Path.ToString();
            var query = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null;

            try
            {
                await next(context);

                sw.Stop();

                // Normal (success veya handled error response) için tek log
                _logger.LogInformation(
                    "HTTP {Method} {Path}{Query} => {StatusCode} in {ElapsedMs}ms)",
                    method, path, query ?? "", context.Response.StatusCode, sw.ElapsedMilliseconds);
            }
            catch
            {
                // Exception'ı yutma; exception middleware yakalasın
                sw.Stop();

                // Burada stacktrace loglamak istemezsen rethrow.
                // Exception middleware zaten LogError yapıyor olabilir; double-log istemiyorsan burada Warning bırak.
                _logger.LogWarning(
                    "HTTP {Method} {Path}{Query} threw exception after {ElapsedMs}ms)",
                    method, path, query ?? "", sw.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
