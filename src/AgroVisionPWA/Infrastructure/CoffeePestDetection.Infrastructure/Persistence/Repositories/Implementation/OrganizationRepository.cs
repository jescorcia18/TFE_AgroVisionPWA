using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _context;

    public OrganizationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Organization>> GetActiveAsync()
    {
        return await _context.Organizations
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Organization?>GetByIdAsync(Guid id)
    {
        return await _context
            .Organizations
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x =>
                x.Id == id
                &&
                x.IsActive);
    }
}
