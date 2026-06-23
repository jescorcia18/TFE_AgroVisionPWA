using CoffeePestDetection.Domain.Entities;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface ISyncLogRepository
{
    Task AddAsync(SyncLog syncLog);

    Task<List<SyncLog>> GetAllAsync();

    Task<List<SyncLog>> GetByDeviceAsync(string deviceId);

    Task<SyncLog?> GetLastByDeviceAsync(string deviceId);
}
