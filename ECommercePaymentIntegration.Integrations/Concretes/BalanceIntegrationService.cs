using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Configurations;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Request;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Response.Items;
using ECommercePaymentIntegration.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ECommercePaymentIntegration.Integrations.Concretes
{
    public class BalanceIntegrationService : IBalanceIntegrationService
    {
        private readonly ILogger<BalanceIntegrationService> _logger;
        private readonly HttpClient _httpClient;
        private readonly BalanceManagementOptions _balanceManagement;
        public BalanceIntegrationService(ILogger<BalanceIntegrationService> logger, HttpClient httpClient, IOptions<BalanceManagementOptions> options)
        {
            _logger = logger;
            _httpClient = httpClient;
            _balanceManagement = options.Value;
            _httpClient.Timeout = TimeSpan.FromMilliseconds(_balanceManagement.Timeoutms);
        }
        public async Task<BalanceBaseResponse<CancelResponse>> CancelOrder(CancelRequest request, CancellationToken cancellation = default)
        {

            string reqBody = System.Text.Json.JsonSerializer.Serialize(request);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"{_balanceManagement.BaseUrl}/api/balance/cancel")
            {
                Content = new StringContent(reqBody, System.Text.Encoding.UTF8, "application/json")
            };


            _logger.LogInformation("Sending CancelOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            using var response = await _httpClient.SendAsync(message, cancellation);

            await EnsureSuccessOrThrow(response, cancellation);

            var result = await response.Content.ReadFromJsonAsync<BalanceBaseResponse<CancelResponse>>(cancellation);

            _logger.LogInformation("Sent CancelOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            return result ?? throw new ExternalServiceException("Body was not readed!");
        }

        public async Task<BalanceBaseResponse<CompleteResponse>> CompleteOrder(CompleteRequest request, CancellationToken cancellation = default)
        {
            string reqBody = System.Text.Json.JsonSerializer.Serialize(request);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"{_balanceManagement.BaseUrl}/api/balance/complete")
            {
                Content = new StringContent(reqBody, System.Text.Encoding.UTF8, "application/json")
            };


            _logger.LogInformation("Sending CompleteOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            using var response = await _httpClient.SendAsync(message, cancellation);

            await EnsureSuccessOrThrow(response, cancellation);

            var result = await response.Content.ReadFromJsonAsync<BalanceBaseResponse<CompleteResponse>>(cancellation);

            _logger.LogInformation("Sent CompleteOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            return result ?? throw new ExternalServiceException("Body was not readed!");
        }

        public async Task<BalanceBaseResponse<BalanceItem>> GetBalance(CancellationToken cancellation = default)
        {
            _logger.LogInformation("Sending GetBalance request to Balance service. /api/balance");

            using var response = await _httpClient.GetAsync($"{_balanceManagement.BaseUrl}/api/balance", cancellation);

            await EnsureSuccessOrThrow(response, cancellation);

            var result = await response.Content.ReadFromJsonAsync<BalanceBaseResponse<BalanceItem>>(cancellation);

            _logger.LogInformation("Sent GetBalance request to Balance service. /api/balance");

            return result ?? throw new ExternalServiceException("Body was not readed!");
        }

        public async Task<BalanceBaseResponse<List<ProductItem>>> GetProducts(CancellationToken cancellation = default) 
        {
            _logger.LogInformation("Sending GetProducts request to Balance service. /api/balance");

            using var response = await _httpClient.GetAsync($"{_balanceManagement.BaseUrl}/api/products", cancellation);

            await EnsureSuccessOrThrow(response, cancellation);

            var result = await response.Content.ReadFromJsonAsync<BalanceBaseResponse<List<ProductItem>>>(cancellation);

            _logger.LogInformation("Sent GetProducts request to Balance service. /api/balance");

            return result ?? throw new ExternalServiceException("Body was not readed!");
        }

        public async Task<BalanceBaseResponse<PreOrderResponse>> PreOrder(PreOrder request, CancellationToken cancellation = default)
        {
            string reqBody = System.Text.Json.JsonSerializer.Serialize(request);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"{_balanceManagement.BaseUrl}/api/balance/preorder")
            {
                Content = new StringContent(reqBody, System.Text.Encoding.UTF8, "application/json")
            };


            _logger.LogInformation("Sending PreOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            using var response = await _httpClient.SendAsync(message, cancellation);

            await EnsureSuccessOrThrow(response, cancellation);

            var result = await response.Content.ReadFromJsonAsync<BalanceBaseResponse<PreOrderResponse>>(cancellation);

            _logger.LogInformation("Sent PreOrder request to Balance service. RequestBody={RequestBody}", reqBody);

            return result ?? throw new ExternalServiceException("Body was not readed!");
        }



        private static async Task EnsureSuccessOrThrow(HttpResponseMessage resp, CancellationToken ct)
        {
            if (resp.IsSuccessStatusCode) return;

            var body = await SafeReadBody(resp, ct);

            throw new ExternalServiceException(message: $"Balance service call failed. Status={(int)resp.StatusCode} {resp.ReasonPhrase}. Body={body}");
        }

        private static async Task<string> SafeReadBody(HttpResponseMessage resp, CancellationToken ct)
        {
            try { return await resp.Content.ReadAsStringAsync(ct); }
            catch { return "<unreadable>"; }
        }


    }
}
