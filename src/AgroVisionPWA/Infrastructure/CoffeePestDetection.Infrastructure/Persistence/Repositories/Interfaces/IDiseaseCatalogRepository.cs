using CoffeePestDetection.Domain.Entities;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IDiseaseCatalogRepository
{
    Task<List<DiseaseCatalog>> GetAllAsync();

    Task<DiseaseCatalog?> GetByIdAsync(Guid id);

    Task AddAsync(DiseaseCatalog disease);

    Task UpdateAsync(DiseaseCatalog disease);

    Task DeleteAsync(DiseaseCatalog disease);

    Task<bool> ExistsByNameAsync(string commonName, Guid? exludeId = null);
}
