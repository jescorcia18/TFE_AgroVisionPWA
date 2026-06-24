using CoffeePestDetection.Domain.Entities;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface ITelemetryRepository
{
    Task AddAsync(Telemetry telemetry);

    Task<Telemetry?> GetByIdAsync(Guid id);

    Task<List<Telemetry>> GetAllAsync();
}
