using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class SyncLogRepository : ISyncLogRepository
{
    private readonly ApplicationDbContext _context;

    public SyncLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SyncLog syncLog)
    {
        await _context.SyncLogs
            .AddAsync(syncLog);
    }

    public async Task<List<SyncLog>> GetAllAsync()
    {
        return await _context.SyncLogs
            .OrderByDescending(x => x.StartedAt)
            .ToListAsync();
    }

    public async Task<List<SyncLog>> GetByDeviceAsync(string deviceId)
    {
        return await _context.SyncLogs
            .Where(x =>
                !x.IsDeleted &&
                x.DeviceId == deviceId)
            .OrderByDescending(x => x.StartedAt)
            .ToListAsync();
    }

    public async Task<SyncLog?> GetLastByDeviceAsync(string deviceId)
    {
        return await _context.SyncLogs
            .Where(x =>
                !x.IsDeleted &&
                x.DeviceId == deviceId)
            .OrderByDescending(x => x.StartedAt)
            .FirstOrDefaultAsync();
    }
}
