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
    public class ObservationRepository: IObservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ObservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Observation observation)
        {
            await _context.Observations
                .AddAsync(observation);
        }

        public async Task<List<Observation>>GetByInspectionIdAsync(
                Guid inspectionId)
        {
            return await _context.Observations
                .Include(x => x.Disease)
                .Where(x =>
                    x.InspectionId == inspectionId &&
                    !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Observations
                .AnyAsync(x => x.Id == id);
        }
    }
}
