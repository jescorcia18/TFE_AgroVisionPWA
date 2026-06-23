using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.Observation.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Authorize]
[Route("api/observations")]
public class ObservationsController: ControllerBase
{
    private readonly IObservationService _service;

    public ObservationsController( IObservationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateObservationRequestDto request)
    {
        var result = await _service.CreateAsync( request);

        return Created("Creado",
            ApiResponse<ObservationDto>
                .Ok(
                    result,
                    "Observación registrada correctamente."));
    }

    [HttpGet("inspection/{inspectionId:guid}")]
    public async Task<IActionResult> GetByInspection( Guid inspectionId)
    {
        var result =await _service.GetByInspectionIdAsync(inspectionId);

        return Ok(
            ApiResponse<List<ObservationDto>>
                .Ok(result));
    }
}
