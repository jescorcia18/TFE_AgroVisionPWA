using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums.Features.Sync;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class SyncLogRepository : ISyncLogRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;

    public SyncLogRepository(ApplicationDbContext context, IServiceScopeFactory scopeFactory)
    {
        _context = context;
        _scopeFactory = scopeFactory;
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

    public async Task MarkAsSuccessAsync(
    Guid id,
    int syncedEntities,
    int inspections,
    int images,
    int observations,
    int inferenceResults,
    int executionTimeMS)
    {
        using var scope = _scopeFactory.CreateScope();

        var context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var syncLog =
            await context.SyncLogs.FindAsync(id);

        if (syncLog is null)
            return;

        syncLog.Status = SyncLogEnum.Status.Success.ToString();
        syncLog.SyncedEntities = syncedEntities;
        syncLog.SyncedInspections = inspections;
        syncLog.SyncedImages = images;
        syncLog.SyncedObservations = observations;
        syncLog.SyncedInferenceResults = inferenceResults;
        syncLog.ExecutionTimeMs = (int)executionTimeMS;
        syncLog.FinishedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task MarkAsFailedAsync(Guid id, string errorMessage, string exceptionName, int executionTimeMs)
    {
        using var scope = _scopeFactory.CreateScope();

        var context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var syncLog =
            await context.SyncLogs.FindAsync(id);

        if (syncLog is null)
            return;

        syncLog.Status = SyncLogEnum.Status.Failed.ToString();
        syncLog.ErrorMessage = errorMessage;
        syncLog.ExecutionTimeMs = (int)executionTimeMs;
        syncLog.ExceptionType = exceptionName;
        syncLog.FinishedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task<SyncLog> CreatePendingAsync(string deviceId)
    {
        var connection = _context.Database.GetDbConnection();
        var servidor = connection.DataSource;
        var bd = connection.Database;

        using var scope = _scopeFactory.CreateScope();

        var context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var syncLog = new SyncLog
        {
            Id = Guid.NewGuid(),
            DeviceId = deviceId,
            Status = SyncLogEnum.Status.Pending.ToString(),
            StartedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await context.SyncLogs.AddAsync(syncLog);

        await context.SaveChangesAsync();

        return syncLog;
    }
}
