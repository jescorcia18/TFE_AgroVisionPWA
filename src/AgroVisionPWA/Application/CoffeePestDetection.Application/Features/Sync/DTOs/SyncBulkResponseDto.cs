
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncBulkResponseDto
{
    public string Message { get; set; } = string.Empty;

    public int SyncedEntities { get; set; }

    public int SyncedInspections { get; set; }

    public int SyncedImages { get; set; }

    public int SyncedObservations { get; set; }

    public int SyncedInferenceResults { get; set; }

    public DateTime Timestamp { get; set; }
}
