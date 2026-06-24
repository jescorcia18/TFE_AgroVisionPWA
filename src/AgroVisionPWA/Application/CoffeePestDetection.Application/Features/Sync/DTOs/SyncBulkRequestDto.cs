

namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncBulkRequestDto
{
    public string DeviceId { get; set; } = string.Empty;

    public List<SyncInspectionDto> Inspections { get; set; } = [];

    public List<SyncImageDto> Images { get; set; } = [];

    public List<SyncObservationDto> Observations { get; set; } = [];

    public List<SyncInferenceResultDto> InferenceResults { get; set; } = [];

    public List<SyncTelemetryDto> Telemetries { get; set; } = [];
}
