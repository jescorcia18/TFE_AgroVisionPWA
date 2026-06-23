using CoffeePestDetection.Domain.Entities;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IObservationRepository
{
    Task AddAsync(Observation observation);

    Task<List<Observation>> GetByInspectionIdAsync(Guid inspectionId);
}
