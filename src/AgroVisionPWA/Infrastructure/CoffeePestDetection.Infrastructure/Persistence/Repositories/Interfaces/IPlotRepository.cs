using CoffeePestDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IPlotRepository
{
    Task<Plot?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<Plot>> GetByFarmAsync(Guid farmId);

    Task AddAsync(Plot plot);

    Task<IReadOnlyList<Plot>> GetByOrganizationAsync(Guid organizationId);
}
