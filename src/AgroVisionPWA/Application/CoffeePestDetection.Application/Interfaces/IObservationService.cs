using CoffeePestDetection.Application.Features.Observation.DTOs;

namespace CoffeePestDetection.Application.Interfaces;

public interface IObservationService
{
    Task<ObservationDto> CreateAsync(CreateObservationRequestDto request);

    Task<List<ObservationDto>> GetByInspectionIdAsync(Guid inspectionId);
}
