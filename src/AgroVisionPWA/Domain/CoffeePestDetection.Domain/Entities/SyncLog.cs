using CoffeePestDetection.Application.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeePestDetection.Domain.Entities;

public class SyncLog : BaseEntity
{
    public string DeviceId { get; set; }
        = string.Empty;

    public int SyncedEntities { get; set; }

    public int SyncedInspections { get; set; }

    public int SyncedImages { get; set; }

    public int SyncedObservations { get; set; }

    public int SyncedInferenceResults { get; set; }

    public string Status { get; set; }= string.Empty;

    public string? ErrorMessage { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    [Column ("exception_type")]
    public string? ExceptionType { get; set; } = string.Empty;

    [Column("execution_time_ms")]
    public int? ExecutionTimeMs { get; set; }
}
