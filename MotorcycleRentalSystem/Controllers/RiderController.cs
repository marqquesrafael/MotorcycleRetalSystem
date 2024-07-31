using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Rider;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly IRiderService _riderService;

        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRiderRequest request)
        {
            try
            {
                await _riderService.Register(request);
                return Ok();
            }
            catch(ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
