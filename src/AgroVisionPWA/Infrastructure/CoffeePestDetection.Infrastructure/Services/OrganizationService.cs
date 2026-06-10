using CoffeePestDetection.Application.Features.Org.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services
{
    public class OrganizationService: IOrganizationService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<OrganizationDto>>GetOrganizationsAsync()
        {
            return await _context
                .Organizations
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x =>
                    new OrganizationDto
                    {
                        Id = x.Id,

                        Name = x.Name
                    })
                .ToListAsync();
        }
    }
}
