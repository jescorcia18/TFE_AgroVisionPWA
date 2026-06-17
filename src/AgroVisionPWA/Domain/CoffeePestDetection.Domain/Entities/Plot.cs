using CoffeePestDetection.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities;

public class Plot : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    /** Tamaño del lote*/
    public double?AreaHectares{ get; set;}
    public bool IsActive{ get; set; } = true;
    /** FK*/
    public Guid FarmId { get; set; }

    /** Navigation */
    public Farm Farm { get; set; } = null!;
    public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

}
