using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation
{
    public class InspectionImageRepository : IInspectionImageRepository
    {
        private readonly ApplicationDbContext _context;

        public InspectionImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InspectionImage image)
        {
            await _context.InspectionImages.AddAsync(image);
        }

        public async Task<bool> ExistsAsync(string fileUri)
        {
            return await _context.InspectionImages
                .AnyAsync(x =>x.FileUri ==fileUri);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.InspectionImages
                .AnyAsync(x => x.Id == id);
        }

        public async Task<InspectionImage?> GetByIdAsync(Guid id)
        {
            return await _context.InspectionImages
                .Include(x => x.Inspection)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<InspectionImage>> GetByInspectionAsync(Guid inspectionId)
        {
            return await _context.InspectionImages
                .Where(x => x.InspectionId == inspectionId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

    }
}
