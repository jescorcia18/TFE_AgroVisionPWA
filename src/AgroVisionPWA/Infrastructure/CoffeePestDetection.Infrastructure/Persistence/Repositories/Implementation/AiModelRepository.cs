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
    public class AiModelRepository : IAiModelRepository
    {
        private readonly ApplicationDbContext _context;
        public AiModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AiModel?>GetCurrentModelAsync()
        {
            return await _context.AiModels
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
