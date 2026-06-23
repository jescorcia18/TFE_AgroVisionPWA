
namespace CoffeePestDetection.Application.Features.Observation.DTOs;

public class CreateObservationRequestDto
{
    public Guid InspectionId { get; set; }

    public Guid DiseaseId { get; set; }

    public int SeverityLevel { get; set; }

    public decimal IncidencePercent { get; set; }

    public string SourceType { get; set; } = string.Empty;
}
