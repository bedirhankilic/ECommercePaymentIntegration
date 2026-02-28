using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePaymentIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController(IProductService _productService) : ControllerBase
    {
        /// <summary>
        /// Gets available products from Balance service
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ProblemDetail))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetail))]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> Get(CancellationToken ct)
        {
            var products = await _productService.GetProductItemsAsync(ct);

            return Ok(products);
        }
    }
}
