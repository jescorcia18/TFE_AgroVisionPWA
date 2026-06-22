using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.InfeResult.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Authorize]
[Route("api/inference-results")]
public class InferenceResultsController : ControllerBase
{
    private readonly IInferenceResultService _service;

    public InferenceResultsController(IInferenceResultService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateInferenceResultRequestDto request)
    {
        var result =
            await _service.CreateAsync(request);

        return Created("Creado",
            ApiResponse<InferenceResultDto>
                .Ok(
                    result,
                    "Resultado de inferencia registrado correctamente."));
    }

    [HttpGet("image/{imageId:guid}")]
    public async Task<IActionResult> GetByImage(Guid imageId)
    {
        var result =
            await _service
                .GetByImageIdAsync(imageId);

        return Ok(
            ApiResponse<InferenceResultDto>
                .Ok(result));
    }
}
