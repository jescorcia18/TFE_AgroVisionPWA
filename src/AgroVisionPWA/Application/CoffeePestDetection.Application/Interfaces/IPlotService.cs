using CoffeePestDetection.Application.Features.Plot.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IPlotService
    {
        Task<IReadOnlyList<PlotDto>> GetPlotsAsync(Guid organizationId);
    }
}
