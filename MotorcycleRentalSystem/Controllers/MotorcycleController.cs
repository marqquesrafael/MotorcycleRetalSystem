using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle;
using MotorcycleRentalSystem.Domain.Reponses.Motorcycle;
using MotorcycleRentalSystem.Domain.Requests;
using MotorcycleRentalSystem.WebApi.Filters;
using System.Net;

namespace MotorcycleRentalSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(
            ILogger<MotorcycleController> logger,
            IMotorcycleService motorcycleService)
        {
            _logger = logger;
            _motorcycleService = motorcycleService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAll()
        {
            var motorcycles = _motorcycleService.GetAll<MotorcycleResponse>();
            return Ok(motorcycles);
        }

        [HttpGet]
        [Route("license-plate")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetByLicensePlate([FromQuery] string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
                return BadRequest("Favor informar uma placa válida");

            var motorcycle = _motorcycleService.GetByLicensePlate(licensePlate);

            if (motorcycle is null)
                return NotFound("Nenhuma moto foi encontrada para a placa informada.");

            return Ok(motorcycle);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(UserAuthenticationAttribute))]
        public async Task<IActionResult> Create([FromBody] AddMotorcycleRequest request)
        {
            try
            {
                var user = HttpContext.Items["userEmail"] as string;
                await _motorcycleService.Register(request, user);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserWithoutPermissionException ex)
            {
                return Forbid(ex.Message);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (MotorcycleAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error [MotorcycleController] on method Create with message: {ex.Message} and stack trace {ex.StackTrace}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro ao registrar a nova moto, favor entrar em contato com o administrador do sistema.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateLicensePlate([FromBody] UpdateLicensePlateRequest request)
        {
            try
            {
                await _motorcycleService.UpdateLicensePlate(request);
                return Ok();
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MotorcycleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
