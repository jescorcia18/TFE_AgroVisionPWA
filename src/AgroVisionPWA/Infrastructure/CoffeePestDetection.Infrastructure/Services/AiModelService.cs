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

        public async Task<FileResultDto> DownloadModelJsonAsync(string webRootPath)
        {
            var model = await _repository.GetCurrentModelAsync();

            if (model is null)
                throw new NotFoundException("No existe modelo activo.");

            // 3. Limpiamos los caracteres de barras iniciales para evitar conflictos con Path.Combine
            string relativePath = model.ModelJsonPath.TrimStart('/', '\\');

            // 4. Combinamos: D:\...\AgroVisionPWA\API\wwwroot + models\coffee_detector\1.0.0\model.json
            string fullPhysicalPath = Path.Combine(webRootPath, relativePath);

            if (!System.IO.File.Exists(fullPhysicalPath))
                throw new NotFoundException("El archivo físico del modelo no se encontró.");

            var bytes = await File.ReadAllBytesAsync(fullPhysicalPath);

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
