using CoffeePestDetection.Application.Features.IA.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IAiModelService
    {
        Task<ModelVersionDto> GetCurrentModelAsync();

        Task<FileResultDto>DownloadModelJsonAsync();

        Task<FileResultDto>DownloadWeightsAsync();
    }
}
