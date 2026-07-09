using CoffeePestDetection.Domain.Entities;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface ISyncLogRepository
{
    Task<SyncLog> CreatePendingAsync(string deviceId);

    Task AddAsync(SyncLog syncLog);

    Task<List<SyncLog>> GetAllAsync();

    Task<List<SyncLog>> GetByDeviceAsync(string deviceId);

    Task<SyncLog?> GetLastByDeviceAsync(string deviceId);

    Task MarkAsSuccessAsync(
        Guid id,
        int syncedEntities,
        int inspections,
        int images,
        int observations,
        int inferenceResults,
        int executionTimeMs);

    Task MarkAsFailedAsync(
        Guid id,
        string errorMessage,
        string exceptionName,
        int executionTimeMs);
}
