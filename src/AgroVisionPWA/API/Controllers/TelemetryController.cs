using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.Telemetry.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;

namespace CoffeePestDetection.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _service;
        public TelemetryController(ITelemetryService service)
        { 
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateTelemetryRequestDto request)
        {
            await _service.CreateAsync(request);

            return Created("Creado",
                ApiResponse<string>.Ok(
                    "Telemetría registrada."));
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var result =await _service.GetAllAsync();

            return Ok(
                ApiResponse<IReadOnlyList<TelemetryDto>>
                    .Ok(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result =
                await _service.GetByIdAsync(id);

            return Ok(
                ApiResponse<TelemetryDto>
                    .Ok(result));
        }
    }
}
