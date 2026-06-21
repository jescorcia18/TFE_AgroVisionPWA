using CoffeePestDetection.Application.Features.Image.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IInspectionImageService
    {
        Task<InspectionImageDto> AddImageAsync(Guid organizationId, CreateInspectionImageRequestDto request);

        Task<IReadOnlyList<InspectionImageDto>> GetImagesAsync(Guid inspectionId, Guid organizationId);
    }
}
