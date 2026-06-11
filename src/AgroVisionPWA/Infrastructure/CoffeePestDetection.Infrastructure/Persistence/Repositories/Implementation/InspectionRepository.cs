using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class InspectionRepository: IInspectionRepository
{
    private readonly
        ApplicationDbContext
        _context;

    public InspectionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Inspection inspection)
    {
        await _context.Inspections.AddAsync(inspection);
    }

    public async Task<Inspection?> GetByIdAsync(Guid id)
    {
        return await _context
            .Inspections
            .Include(x => x.Inspector)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
