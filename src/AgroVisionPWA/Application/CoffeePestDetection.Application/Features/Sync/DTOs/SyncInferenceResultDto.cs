
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncInferenceResultDto
{
    public Guid Id { get; set; }

    public Guid ImageId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public string ModelVersion { get; set; } = string.Empty;

    public Guid PredictedDiseaseId { get; set; }

    public decimal Confidence { get; set; }

    public string? TopKJson { get; set; }

    public int? InferenceTimeMs { get; set; }

    public string? TfBackend { get; set; }

    public decimal? DeviceMemoryGb { get; set; }

    public string? Browser { get; set; }

    public string? BrowserVersion { get; set; }

    public string? Platform { get; set; }

    public string? OperatingSystem { get; set; }

    public string? UserAgent { get; set; }

    public string? TensorflowVersion { get; set; }
}
