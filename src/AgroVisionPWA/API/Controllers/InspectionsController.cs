using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Extensions;
using CoffeePestDetection.Application.Features.Inspect.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Authorize]
[Route("api/inspections")]

public class InspectionsController : ControllerBase
{
    private readonly IInspectionService _service;

    public InspectionsController(IInspectionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInspectionRequestDto request)
    {
        var userId = User.GetUserId();
        var organizationId = User.GetOrganizationId();

        var result = await _service.CreateAsync(userId, organizationId, request);

        return CreatedAtAction(nameof(GetById),
            new { id = result.Id },
            ApiResponse<CreateInspectionResponseDto>
            .Ok(
                result, "Inspección creada exitosamente"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(ApiResponse<CreateInspectionResponseDto>
            .Ok(result!, "Inspección obtenida exitosamente."));
    }

    [HttpPut("{id}/plot")]
    public async Task<IActionResult> AssignPlot(Guid id, AssignPlotRequestDto request)
    {
        var organizationId = User.GetOrganizationId();

        await _service.AssignPlotAsync(id,request.PlotId,organizationId);

        return Ok(ApiResponse<string>
            .Ok("Inspección asociada correctamente."));
    }
}
