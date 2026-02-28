using ECommercePaymentIntegration.Shared.DTO;
using ECommercePaymentIntegration.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ECommercePaymentIntegration.Shared.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger)
    {
        private const string ProblemJson = "application/problem+json";

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var correlationId = context.TraceIdentifier;
                var path = context.Request.Path.ToString();

                // Log (structured olsun diye code/ids vs. eklendi)
                _logger.LogError(ex, "Unhandled exception. CorrelationId={CorrelationId} Path={Path}", correlationId, path);

                var (statusCode, pd) = MapToProblemDetails(ex, path, correlationId);

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = ProblemJson;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(pd));
            }
            return;
        }

        private static (int Status, ProblemDetail Pd) MapToProblemDetails(Exception ex, string path, string correlationId)
        {
            return ex switch
            {
                NotFoundException notFoundEx => ((int)HttpStatusCode.NotFound, NewProblem((int)HttpStatusCode.NotFound, notFoundEx.Message, notFoundEx.ErrorCode, path, correlationId, notFoundEx.Errors)),
                ExternalServiceException extEx => ((int)HttpStatusCode.BadGateway, NewProblem((int)HttpStatusCode.BadGateway, extEx.Message, extEx.ErrorCode, path, correlationId, extEx.Errors)),
                DomainException domainEx => ((int)HttpStatusCode.BadRequest, NewProblem((int)HttpStatusCode.BadRequest, domainEx.Message, domainEx.ErrorCode, path, correlationId, domainEx.Errors)),
                TaskCanceledException canceledEx => ((int)HttpStatusCode.RequestTimeout, NewProblem((int)HttpStatusCode.RequestTimeout, "The request was canceled due to timeout.", "request_timeout", path, correlationId, null)),
                Exceptions.ApplicationException appEx => ((int)HttpStatusCode.BadRequest, NewProblem((int)HttpStatusCode.BadRequest, appEx.Message, appEx.ErrorCode, path, correlationId, appEx.Errors)),
                _ => ((int)HttpStatusCode.InternalServerError, NewProblem((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.", "internal_server_error", path, correlationId, null)),
            };
        }

        private static ProblemDetail NewProblem(int status, string message, string code, string path, string correlationId, List<string>? errors)
        {
            var pd = new ProblemDetail
            {
                status = status,
                message = message,
                path = path,
                code = code,
                errors = errors,
                correlationId = correlationId
            };
            return pd;
        }
    }
}
