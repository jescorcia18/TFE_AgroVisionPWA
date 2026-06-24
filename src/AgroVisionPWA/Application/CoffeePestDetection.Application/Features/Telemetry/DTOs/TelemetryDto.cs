namespace CoffeePestDetection.Application.Features.Telemetry.DTOs;

public class TelemetryDto
{
    public Guid Id { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string PestType { get; set; } = string.Empty;

    public decimal Confidence { get; set; }

    public int? InferenceTimeMs { get; set; }

    public int? InspectionCount { get; set; }

    public string? DeviceHash { get; set; }

    public string SyncStatus { get; set; } = string.Empty;
}
