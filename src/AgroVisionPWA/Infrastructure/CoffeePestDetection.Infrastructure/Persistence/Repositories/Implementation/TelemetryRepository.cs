using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class TelemetryRepository : ITelemetryRepository
{
    private readonly ApplicationDbContext _context;

    public TelemetryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Telemetry telemetry)
    {
        await _context.Telemetries.AddAsync(telemetry);
    }

    public async Task<Telemetry?> GetByIdAsync(Guid id)
    {
        return await _context.Telemetries
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);
    }

    public async Task<List<Telemetry>> GetAllAsync()
    {
        return await _context.Telemetries
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }
}
