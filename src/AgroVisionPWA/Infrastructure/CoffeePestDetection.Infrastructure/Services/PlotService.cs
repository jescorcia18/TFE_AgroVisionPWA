using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Plot.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services
{
    public class PlotService : IPlotService
    {
        public readonly IPlotRepository _repo;
        public PlotService(IPlotRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<PlotDto>> GetPlotsAsync(Guid organizationId)
        {
            var plots = await _repo.GetByOrganizationAsync(organizationId);

            if (plots == null)
                throw new NotFoundException($"No hay lotes.");

            return plots
                .Select(x => new PlotDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    FarmName = x.Farm.Name
                }).ToList();
        }
    }
}
