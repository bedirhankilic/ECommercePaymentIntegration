using ECommercePaymentIntegration.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = ECommercePaymentIntegration.Shared.Exceptions.ApplicationException;

namespace ECommercePaymentIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IIdentityService _service) : ControllerBase
    {
        /// <summary>
        /// Login to get access token
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationException))]
        public ActionResult Login([FromForm] Domain.DTO.Request.LoginRequest req)
        {
            var res = _service.Login(req);
            return Ok(res);
        }
    }
}
