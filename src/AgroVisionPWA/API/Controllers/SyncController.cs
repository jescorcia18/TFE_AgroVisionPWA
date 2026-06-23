using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.Sync.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Authorize]
[Route("api/sync")]
public class SyncController : ControllerBase
{
    private readonly ISyncService _service;

    public SyncController(ISyncService service)
    {
        _service = service;
    }

    [HttpPost("bulk")]
    public async Task<IActionResult>Bulk( SyncBulkRequestDto request)
    {
        var result = await _service.SyncBulkAsync(request);

        return Ok(
            ApiResponse<SyncBulkResponseDto>
                .Ok(
                    result,
                    "Sincronización completada correctamente."));
    }

    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs()
    {
        var result =await _service .GetLogsAsync();

        return Ok(
            ApiResponse<List<SyncLogDto>>
                .Ok(
                    result,
                    "Logs de sincronización obtenidos correctamente."));
    }

    [HttpGet("logs/device/{deviceId}")]
    public async Task<IActionResult>GetByDevice( string deviceId)
    {
        var result =await _service.GetLogsByDeviceAsync( deviceId);

        return Ok(
            ApiResponse<List<SyncLogDto>>
                .Ok(
                    result,
                    "Logs obtenidos correctamente."));
    }

    [HttpGet("logs/device/{deviceId}/last")]
    public async Task<IActionResult>GetLastByDevice(string deviceId)
    {
        var result =await _service.GetLastLogByDeviceAsync(deviceId);

        return Ok(
            ApiResponse<SyncLogDto?>
                .Ok(
                    result,
                    "Última sincronización obtenida correctamente."));
    }
}
