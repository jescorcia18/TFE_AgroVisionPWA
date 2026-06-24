using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeePestDetection.Application.Features.InfeResult.DTOs;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Application.Exceptions;
using System.Xml.XPath;

namespace CoffeePestDetection.Infrastructure.Services;

public class InferenceResultService : IInferenceResultService
{
    private readonly IInferenceResultRepository _repository;

    private readonly IInspectionImageRepository _imageRepository;

    private readonly IDiseaseCatalogRepository _diseaseRepository;

    private readonly ApplicationDbContext _context;

    public InferenceResultService(
        IInferenceResultRepository repository,
        IInspectionImageRepository imageRepository,
        IDiseaseCatalogRepository diseaseRepository,
        ApplicationDbContext context)
    {
        _repository = repository;
        _imageRepository = imageRepository;
        _diseaseRepository = diseaseRepository;
        _context = context;
    }

    public async Task<InferenceResultDto> CreateAsync(CreateInferenceResultRequestDto request)
    {
        await ValidateRequest(request);

        var disease =
            await _diseaseRepository
            .GetByIdAsync(request.PredictedDiseaseId);

        var inference =
            new InferenceResult
            {
                Id = Guid.NewGuid(),
                ImageId = request.ImageId,
                PredictedDiseaseId = request.PredictedDiseaseId,
                ModelName = request.ModelName.Trim(),
                ModelVersion = request.ModelVersion.Trim(),
                Confidence = request.Confidence,
                TopKJson = request.TopKJson,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

        await _repository.AddAsync(inference);

        await _context.SaveChangesAsync();

        return new InferenceResultDto
        {
            Id = inference.Id,
            ImageId = inference.ImageId,
            PredictedDiseaseId = inference.PredictedDiseaseId,
            DiseaseName = disease!.CommonName,
            ModelName = inference.ModelName,
            ModelVersion = inference.ModelVersion,
            InferenceTimeMs = inference.InferenceTimeMs,
            TfBackend = inference.TfBackend,
            DeviceMemoryGb = inference.DeviceMemoryGb,
            Confidence = inference.Confidence,
            CreatedAt = inference.CreatedAt,
            Recommendation = string.IsNullOrEmpty(disease.Recommendation) ? GetRecommendation():disease.Recommendation
        };
    }

    public async Task<InferenceResultDto> GetByImageIdAsync(
        Guid imageId)
    {
        var result = await _repository
            .GetByImageIdAsync(imageId);

        if (result is null)
        {
            throw new NotFoundException("No existe resultado de inferencia para la imagen.");
        }

        return new InferenceResultDto
        {
            Id = result.Id,
            ImageId = result.ImageId,
            PredictedDiseaseId = result.PredictedDiseaseId,
            DiseaseName = result.PredictedDisease.CommonName,
            ModelName = result.ModelName,
            ModelVersion = result.ModelVersion,
            InferenceTimeMs = result.InferenceTimeMs,
            TfBackend = result.TfBackend,
            DeviceMemoryGb = result.DeviceMemoryGb,
            Confidence = result.Confidence,
            CreatedAt = result.CreatedAt,
            Recommendation = string.IsNullOrEmpty(result.PredictedDisease.Recommendation) ? GetRecommendation() : result.PredictedDisease.Recommendation
        };
    }

    private async Task ValidateRequest(CreateInferenceResultRequestDto request)
    {
        if (request.ImageId == Guid.Empty)
        {
            throw new BadRequestException("La imagen es obligatoria.");
        }

        if (request.PredictedDiseaseId == Guid.Empty)
        {
            throw new BadRequestException("La enfermedad es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.ModelName))
        {
            throw new BadRequestException("El nombre del modelo es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.ModelVersion))
        {
            throw new BadRequestException("La versión del modelo es obligatoria.");
        }

        if (request.Confidence < 0 || request.Confidence > 1)
        {
            throw new BadRequestException("La confianza debe estar entre 0 y 1.");
        }

        var image = await _imageRepository
            .GetByIdAsync(request.ImageId);

        if (image is null)
        {
            throw new NotFoundException("La imagen no existe.");
        }

        var disease = await _diseaseRepository
            .GetByIdAsync(request.PredictedDiseaseId);

        if (disease is null)
        {
            throw new NotFoundException("La enfermedad no existe.");
        }

        var exists = await _repository
            .ExistsByImageIdAsync(request.ImageId);

        if (exists)
        {
            throw new BadRequestException("La imagen ya posee un resultado de inferencia.");
        }
    }

    private static string GetRecommendation()
    {
        return "Consultar con un técnico agrícola para validar el diagnóstico.";
    }

}
