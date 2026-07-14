using CoffeePestDetection.Application.Commons;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeePestDetection.Domain.Entities;

public class Inspection : BaseEntity
{

    [Column("inspector_id")]
    public Guid InspectorId { get; set; }

    [Column("inspection_date")]
    public DateTime InspectionDate { get; set; }

    [Column("sync_status")]
    public string Status { get; set; } = string.Empty;

    [Column("plot_id")]
    public Guid? PlotId { get; set; }

    [Column("organization_id")]
    public Guid OrganizationId { get; set; }

    /* Navigation */

    public Profile Inspector { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
    public Plot? Plot { get; set; }
    public ICollection<InspectionImage> Images { get; set; } = new List<InspectionImage>();

    public ICollection<Observation> Observations { get; set; } = new List<Observation>();

    public ICollection<Telemetry> Telemetries { get; set; } = new List<Telemetry>();
}