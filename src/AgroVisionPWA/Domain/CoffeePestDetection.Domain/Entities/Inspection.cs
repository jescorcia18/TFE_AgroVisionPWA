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

    //[Column("plot_id")]
    //public string PlotId { get; set; } = string.Empty;

    /*
        Navigation 
    */

    public Profile Inspector { get; set; } = null!;
}