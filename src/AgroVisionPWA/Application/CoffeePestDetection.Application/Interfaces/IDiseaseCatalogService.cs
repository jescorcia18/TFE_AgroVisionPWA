using CoffeePestDetection.Application.Features.DiseaseCat.DTOs;


namespace CoffeePestDetection.Application.Interfaces;

public interface IDiseaseCatalogService
{
    Task<IReadOnlyList<DiseaseCatalogDto>> GetAllAsync();

    Task<DiseaseCatalogDto> GetByIdAsync(Guid id);

    Task<DiseaseCatalogDto> CreateAsync(DiseaseCatalogRequestDto request);

    Task UpdateAsync(Guid id, DiseaseCatalogRequestDto request);

    Task DeleteAsync(Guid id);
}
