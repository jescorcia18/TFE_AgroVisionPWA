using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class InferenceResultRepository: IInferenceResultRepository
{
    private readonly ApplicationDbContext _context;

    public InferenceResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(InferenceResult result)
    {
        await _context.InferenceResults
            .AddAsync(result);
    }

    public async Task<InferenceResult?> GetByIdAsync(Guid id)
    {
        return await _context.InferenceResults
            .Include(x => x.PredictedDisease)
            .FirstOrDefaultAsync(
                x => x.Id == id &&!x.IsDeleted);
    }

    public async Task<InferenceResult?>
        GetByImageIdAsync(Guid imageId)
    {
        return await _context.InferenceResults
            .Include(x => x.PredictedDisease)
            .FirstOrDefaultAsync(
                x =>x.ImageId == imageId && !x.IsDeleted);
    }

    public async Task<InspectionImage?> GetByIdImageAsync(Guid id)
    {
        return await _context.InspectionImages
            .FirstOrDefaultAsync(x =>
                x.Id == id);
    }

    public async Task<bool> ExistsByImageIdAsync( Guid imageId)
    {
        return await _context.InferenceResults
            .AnyAsync(x =>
                x.ImageId == imageId &&
                !x.IsDeleted);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.InferenceResults
            .AnyAsync(x => x.Id == id);
    }
}
