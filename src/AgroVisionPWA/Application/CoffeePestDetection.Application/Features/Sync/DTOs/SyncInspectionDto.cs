
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncInspectionDto
{
    public Guid Id { get; set; }

    public Guid PlotId { get; set; }

    public Guid InspectorId { get; set; }

    public DateTime InspectionDate { get; set; }
}
