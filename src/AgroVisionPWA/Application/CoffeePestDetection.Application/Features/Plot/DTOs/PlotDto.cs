using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Plot.DTOs;

public class PlotDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FarmName { get; set; } = string.Empty;
}
