using CoffeePestDetection.Application.Features.Inspect.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IInspectionService
    {
        Task<CreateInspectionResponseDto> CreateAsync(Guid userId, CreateInspectionRequestDto request);
        Task<CreateInspectionResponseDto?> GetByIdAsync(Guid id);
    }
}
