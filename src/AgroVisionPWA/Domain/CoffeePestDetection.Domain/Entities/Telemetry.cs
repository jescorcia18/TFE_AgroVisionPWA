using CoffeePestDetection.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities;

public class Telemetry : BaseEntity
{
    public DateTimeOffset Timestamp { get; set; }

    public string PestType { get; set; } = string.Empty;

    public decimal Confidence { get; set; }

    public int? InferenceTimeMs { get; set; }

    public int? InspectionCount { get; set; }

    public string? DeviceHash { get; set; }

    public string SyncStatus { get; set; } = "Pending";
}
