
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
}
