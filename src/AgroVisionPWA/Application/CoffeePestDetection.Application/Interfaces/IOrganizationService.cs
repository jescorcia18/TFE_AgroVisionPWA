using CoffeePestDetection.Application.Features.Org.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<OrganizationDto>>GetOrganizationsAsync();
    }
}
