
using CoffeePestDetection.Application.Commons;
using System.ComponentModel.DataAnnotations.Schema;

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


    [Column("browser")]
    public string? Browser { get; set; }

    [Column("browser_version")]
    public string? BrowserVersion { get; set; }

    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("platform")]
    public string? Platform { get; set; }

    [Column("operating_system")]
    public string? OperatingSystem { get; set; }

    [Column("tensorflow_version")]
    public string? TensorflowVersion { get; set; }

    // Navigation Properties

    public InspectionImage Image { get; set; } = null!;

    public DiseaseCatalog PredictedDisease { get; set; } = null!;
}
