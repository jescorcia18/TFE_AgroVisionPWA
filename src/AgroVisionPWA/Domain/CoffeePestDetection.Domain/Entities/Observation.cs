using CoffeePestDetection.Application.Commons;

namespace CoffeePestDetection.Domain.Entities;

public class Observation : BaseEntity
{
    public Guid InspectionId { get; set; }

    public Guid DiseaseId { get; set; }

    public int SeverityLevel { get; set; }

    public decimal IncidencePercent { get; set; }

    public string SourceType { get; set; } = string.Empty;

    public Inspection Inspection { get; set; } = null!;

    public DiseaseCatalog Disease { get; set; } = null!;
}
