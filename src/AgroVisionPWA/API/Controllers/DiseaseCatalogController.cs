using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.DiseaseCat.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Route("api/disease-catalog")]
[Authorize]
public class DiseaseCatalogController : ControllerBase
{
    private readonly IDiseaseCatalogService _service;

    public DiseaseCatalogController(IDiseaseCatalogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAllAsync();

        return Ok(
            ApiResponse<IReadOnlyList<DiseaseCatalogDto>>
                .Ok(response));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _service.GetByIdAsync(id);

        return Ok(
            ApiResponse<DiseaseCatalogDto>
                .Ok(response));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        DiseaseCatalogRequestDto request)
    {
        var response = await _service.CreateAsync(request);

        return Created("Success",
            ApiResponse<DiseaseCatalogDto>
                .Ok(
                    response,
                    "Enfermedad creada correctamente."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, DiseaseCatalogRequestDto request)
    {
        await _service.UpdateAsync(id, request);

        return Ok(
            ApiResponse<string>
                .Ok("Enfermedad actualizada correctamente."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(
            ApiResponse<string>
                .Ok("Enfermedad eliminada correctamente."));
    }
}
