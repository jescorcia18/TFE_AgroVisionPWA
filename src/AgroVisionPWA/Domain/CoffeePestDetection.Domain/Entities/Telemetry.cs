using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Inspection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities;

public class Telemetry : BaseEntity
{
    [Column("inspection_id")]
    public Guid InspectionId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string PestType { get; set; } = string.Empty;

    public decimal Confidence { get; set; }

    public int? InferenceTimeMs { get; set; }

    public int? InspectionCount { get; set; }

    public string? DeviceHash { get; set; }

    public string SyncStatus { get; set; } = InspectionEnum.Status.Pending.GetDescription();

    // Navigation
    public Inspection Inspection { get; set; } = null!;
}
