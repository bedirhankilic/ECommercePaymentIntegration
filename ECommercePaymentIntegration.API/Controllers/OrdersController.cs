using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePaymentIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IOrdersService _ordersService) : ControllerBase
    {
        /// <summary>
        /// Create a new order with product list and reserve funds
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetail))]
        public async Task<ActionResult<OrderCreateResponse>> Create([FromBody] CreateOrderRequest req, CancellationToken ct)
        {
            var products = await _ordersService.CreateOrderAsync(req, ct);

            return Ok(products);
        }


        /// <summary>
        /// Complete an order and finalize payment
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetail))]
        [Route("{id}/complete")]
        public async Task<ActionResult<OrderCreateResponse>> Complete([FromRoute] string id, CancellationToken ct)
        {
            var products = await _ordersService.CompleteOrderAsync(id, ct);

            return Ok(products);
        }

    }
}
