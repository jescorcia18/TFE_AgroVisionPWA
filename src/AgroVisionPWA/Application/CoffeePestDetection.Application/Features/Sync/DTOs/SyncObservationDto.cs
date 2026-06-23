
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncObservationDto
{
    public Guid Id { get; set; }

    public Guid InspectionId { get; set; }

    public Guid DiseaseId { get; set; }

    public int SeverityLevel { get; set; }

    public decimal IncidencePercent { get; set; }

    public string SourceType { get; set; } = string.Empty;
}
