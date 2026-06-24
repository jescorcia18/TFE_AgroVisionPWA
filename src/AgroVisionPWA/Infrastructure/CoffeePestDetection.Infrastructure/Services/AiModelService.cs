using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.IA.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services
{
    public class AiModelService : IAiModelService
    {
        private readonly IAiModelRepository _repository;
        public AiModelService(IAiModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<ModelVersionDto> GetCurrentModelAsync()
        {
            var model = await _repository.GetCurrentModelAsync();

            if (model is null)
            {
                throw new NotFoundException("No existe un modelo activo.");
            }

            return new ModelVersionDto
            {
                ModelName = model.ModelName,
                Version = model.Version,
                ModelJsonPath = model.ModelJsonPath,
                Checksum = model.Checksum,
                ReleasedAt = model.CreatedAt
            };
        }

        public async Task<FileResultDto> DownloadModelJsonAsync()
        {
            var model = await _repository.GetCurrentModelAsync();

            if (model is null)
                throw new NotFoundException("No existe modelo activo.");

            var bytes = await File.ReadAllBytesAsync(model.ModelJsonPath);

            return new FileResultDto
            {
                Content = bytes,
                ContentType = "application/json",
                FileName = "model.json"
            };
        }

        public async Task<FileResultDto>DownloadWeightsAsync()
        {
            var model = await _repository.GetCurrentModelAsync();

            if (model is null)
                throw new NotFoundException( "No existe modelo activo.");

            var bytes =
                await File.ReadAllBytesAsync( model.WeightsPath);

            return new FileResultDto
            {
                Content = bytes,
                ContentType = "application/octet-stream",
                FileName = "weights.bin"
            };
        }
    }
}
