using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class PlotRepository: IPlotRepository
{
    private readonly ApplicationDbContext _context;

    public PlotRepository(ApplicationDbContext context)
    {
        _context =context;
    }

    public async Task<Plot?>GetByIdAsync(Guid id)
    {
        return await _context.Plots
            .Include(x => x.Farm)
            .FirstOrDefaultAsync( x => x.Id == id);
    }

    public async Task<IReadOnlyList<Plot>>GetByFarmAsync(Guid farmId)
    {
        return await _context.Plots
            .Where( x =>x.FarmId == farmId && x.IsActive)
            .OrderBy( x => x.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Plot plot)
    {
        await _context.Plots
            .AddAsync(plot);
    }

    public async Task<IReadOnlyList<Plot>>GetByOrganizationAsync(Guid organizationId)
    {
        return await _context.Plots
            .Include(x => x.Farm)
            .Where(x => x.IsActive && x.Farm.OrganizationId == organizationId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}
