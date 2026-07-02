using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class DiseaseCatalogRepository : IDiseaseCatalogRepository
{
    private readonly ApplicationDbContext _context;

    public DiseaseCatalogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DiseaseCatalog>> GetAllAsync()
    {
        var result=  await _context.DiseaseCatalogs
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.CommonName)
            .ToListAsync();

        return result;
    }

    public async Task<DiseaseCatalog?> GetByIdAsync(Guid id)
    {
        return await _context.DiseaseCatalogs
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task AddAsync(DiseaseCatalog disease)
    {
        await _context.DiseaseCatalogs.AddAsync(disease);
    }

    public Task UpdateAsync(DiseaseCatalog disease)
    {
        disease.UpdatedAt = DateTime.UtcNow;

        _context.DiseaseCatalogs.Update(disease);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(DiseaseCatalog disease)
    {
        disease.IsDeleted = true;

        disease.UpdatedAt = DateTime.UtcNow;

        _context.DiseaseCatalogs.Update(disease);

        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByNameAsync(string commonName, Guid? excludeId = null)
    {
        commonName = commonName.Trim().ToUpper();

        var query = _context.DiseaseCatalogs
            .Where(x => !x.IsDeleted && x.CommonName.ToUpper() == commonName);

        if (excludeId.HasValue)
        {
            query = query
                .Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }
}
