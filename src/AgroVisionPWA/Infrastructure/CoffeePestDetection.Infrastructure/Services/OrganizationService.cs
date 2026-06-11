using CoffeePestDetection.Application.Features.Org.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repo;

        public OrganizationService(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<OrganizationDto>> GetOrganizationsAsync()
        {
            var organizations = await _repo.GetActiveAsync();

            return organizations.Select(x =>
              new OrganizationDto
              {
                  Id = x.Id,
                  Name = x.Name
              }).ToList();
        }
    }
}
