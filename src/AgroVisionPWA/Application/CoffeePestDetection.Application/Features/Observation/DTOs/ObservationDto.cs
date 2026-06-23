using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Observation.DTOs;

public class ObservationDto
{
    public Guid Id { get; set; }

    public Guid InspectionId { get; set; }

    public Guid DiseaseId { get; set; }

    public string DiseaseName { get; set; } = string.Empty;

    public int SeverityLevel { get; set; }

    public decimal IncidencePercent { get; set; }

    public string SourceType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
