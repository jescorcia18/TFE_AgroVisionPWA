using CoffeePestDetection.Application.Features.InfeResult.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces;

public interface IInferenceResultService
{
    Task<InferenceResultDto> CreateAsync(CreateInferenceResultRequestDto request);

    Task<InferenceResultDto> GetByImageIdAsync(Guid imageId);
}
