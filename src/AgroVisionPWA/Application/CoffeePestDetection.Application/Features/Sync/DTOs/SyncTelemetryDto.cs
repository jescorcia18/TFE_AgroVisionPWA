
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncTelemetryDto
{
    public Guid InspectionId { get; set; }
    public Guid Id { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string PestType { get; set; } = string.Empty;

    public decimal Confidence { get; set; }

    public int? InferenceTimeMs { get; set; }

    public int? InspectionCount { get; set; }

    public string? DeviceHash { get; set; }
}
