using CoffeePestDetection.Domain.Entities;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IInferenceResultRepository
{
    Task AddAsync(InferenceResult result);

    Task<InferenceResult?> GetByImageIdAsync(Guid imageId);

    Task<InferenceResult?> GetByIdAsync(Guid id);

    Task<InspectionImage?> GetByIdImageAsync(Guid id);

    Task<bool> ExistsByImageIdAsync(Guid imageId);
}
