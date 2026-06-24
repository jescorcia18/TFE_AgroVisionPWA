using CoffeePestDetection.Domain.Entities;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IAiModelRepository
{
    Task<AiModel?> GetCurrentModelAsync();
}
