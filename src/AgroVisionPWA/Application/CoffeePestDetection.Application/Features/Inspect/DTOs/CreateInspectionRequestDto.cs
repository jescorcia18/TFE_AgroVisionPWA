using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Inspect.DTOs;

public class CreateInspectionRequestDto
{
    public DateTime InspectionDate { get; set; }

    public Guid PlotId { get; set; }
}
