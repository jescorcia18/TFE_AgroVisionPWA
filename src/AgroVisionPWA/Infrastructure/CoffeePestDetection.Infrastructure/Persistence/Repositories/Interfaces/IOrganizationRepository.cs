using CoffeePestDetection.Domain.Entities;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOrganizationRepository
{
    Task<IReadOnlyList<Organization>>GetActiveAsync();

    Task<Organization?>GetByIdAsync(Guid id);
}
