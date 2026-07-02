
using CoffeePestDetection.Application.Commons;

namespace CoffeePestDetection.Domain.Entities;

public class InferenceResult : BaseEntity
{
    public Guid ImageId { get; set; }

    public Guid PredictedDiseaseId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public string ModelVersion { get; set; } = string.Empty;

    public decimal? Confidence { get; set; }

    public string? TopKJson { get; set; }

    public int? InferenceTimeMs { get; set; }

    public string? TfBackend { get; set; }

    public decimal? DeviceMemoryGb { get; set; }

    // Navigation Properties

    public InspectionImage Image { get; set; } = null!;

    public DiseaseCatalog PredictedDisease { get; set; } = null!;
}
