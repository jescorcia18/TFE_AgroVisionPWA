namespace CoffeePestDetection.Application.Features.Inspect.DTOs;

public class InspectionDetailDto
{
    public Guid Id { get; set; }

    public DateTime InspectionDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public Guid InspectorId { get; set; }

    public string InspectorName { get; set; } = string.Empty;

    public Guid? PlotId { get; set; }

    public string? PlotName { get; set; }

    public int ImageCount { get; set; }

    public int ObservationCount { get; set; }

    public bool HasImages { get; set; }

    public bool HasObservations { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
