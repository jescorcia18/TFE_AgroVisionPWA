using CoffeePestDetection.Domain.Entities;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IInspectionRepository
{
    Task AddAsync(Inspection inspection);

    Task<Inspection?> GetByIdAsync(Guid id);
}
