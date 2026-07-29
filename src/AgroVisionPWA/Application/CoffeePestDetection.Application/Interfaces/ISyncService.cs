using CoffeePestDetection.Application.Features.Sync.DTOs;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces;

public interface ISyncService
{
    Task<SyncBulkResponseDto> SyncBulkAsync(SyncBulkRequestDto request, Guid organizationId);
    Task<List<SyncLogDto>> GetLogsAsync();
    Task<List<SyncLogDto>?> GetLogsByDeviceAsync(string deviceId);

    Task<SyncLogDto?>GetLastLogByDeviceAsync(string deviceId);
}
