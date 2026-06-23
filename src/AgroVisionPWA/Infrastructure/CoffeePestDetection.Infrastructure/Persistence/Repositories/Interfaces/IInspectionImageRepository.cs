using CoffeePestDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IInspectionImageRepository
    {
        Task AddAsync(InspectionImage image);
        Task<InspectionImage?>GetByIdAsync(Guid id);
        Task<IReadOnlyList<InspectionImage>>GetByInspectionAsync(Guid inspectionId);
        Task<bool>ExistsAsync(string fileUri);
        Task<bool> ExistsAsync(Guid id);
    }
}
