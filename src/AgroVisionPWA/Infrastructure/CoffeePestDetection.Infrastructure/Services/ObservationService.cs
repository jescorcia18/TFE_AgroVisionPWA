using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeePestDetection.Application.Features.Observation.DTOs;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Application.Exceptions;

namespace CoffeePestDetection.Infrastructure.Services;

public class ObservationService : IObservationService
{
    private readonly IObservationRepository _repository;

    private readonly IInspectionRepository _inspectionRepository;

    private readonly IDiseaseCatalogRepository _diseaseRepository;

    private readonly ApplicationDbContext _context;

    public ObservationService(
        IObservationRepository repository,
        IInspectionRepository inspectionRepository,
        IDiseaseCatalogRepository diseaseRepository,
        ApplicationDbContext context)
    {
        _repository = repository;
        _inspectionRepository = inspectionRepository;
        _diseaseRepository = diseaseRepository;
        _context = context;
    }

    public async Task<ObservationDto> CreateAsync(CreateObservationRequestDto request)
    {
        await ValidateRequest(request);

        var disease = await _diseaseRepository.GetByIdAsync(request.DiseaseId);

        var observation =
            new Observation
            {
                Id = Guid.NewGuid(),
                InspectionId = request.InspectionId,
                DiseaseId = request.DiseaseId,
                SeverityLevel = request.SeverityLevel,
                IncidencePercent = request.IncidencePercent,
                SourceType = request.SourceType,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

        await _repository.AddAsync(observation);

        await _context.SaveChangesAsync();

        return new ObservationDto
        {
            Id = observation.Id,
            InspectionId = observation.InspectionId,
            DiseaseId = observation.DiseaseId,
            DiseaseName = disease!.CommonName,
            SeverityLevel = observation.SeverityLevel,
            IncidencePercent = observation.IncidencePercent,
            SourceType = observation.SourceType,
            CreatedAt = observation.CreatedAt
        };
    }

    public async Task<List<ObservationDto>>
    GetByInspectionIdAsync(Guid inspectionId)
    {
        var observations = await _repository
            .GetByInspectionIdAsync(inspectionId);

        return observations
            .Select(x => new ObservationDto
            {
                Id = x.Id,
                InspectionId = x.InspectionId,
                DiseaseId = x.DiseaseId,
                DiseaseName = x.Disease.CommonName,
                SeverityLevel = x.SeverityLevel,
                IncidencePercent = x.IncidencePercent,
                SourceType = x.SourceType,
                CreatedAt = x.CreatedAt
            })
            .ToList();
    }

    private async Task ValidateRequest(CreateObservationRequestDto request)
    {
        if (request.InspectionId == Guid.Empty)
        {
            throw new BadRequestException("La inspección es obligatoria.");
        }

        if (request.DiseaseId == Guid.Empty)
        {
            throw new BadRequestException("La enfermedad es obligatoria.");
        }

        if (request.SeverityLevel < 1 || request.SeverityLevel > 5)
        {
            throw new BadRequestException("La severidad debe estar entre 1 y 5.");
        }

        if (request.IncidencePercent < 0 || request.IncidencePercent > 100)
        {
            throw new BadRequestException("La incidencia debe estar entre 0 y 100.");
        }

        var inspection = await _inspectionRepository
                .GetByIdAsync(request.InspectionId);

        if (inspection is null)
        {
            throw new NotFoundException("La inspección no existe.");
        }

        var disease = await _diseaseRepository
                .GetByIdAsync(request.DiseaseId);

        if (disease is null)
        {
            throw new NotFoundException("La enfermedad no existe.");
        }
    }
}