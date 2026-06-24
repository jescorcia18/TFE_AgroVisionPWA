using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Telemetry.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services;

public class TelemetryService : ITelemetryService
{
    private readonly ITelemetryRepository _repository;
    private readonly ApplicationDbContext _context;

    public TelemetryService(ITelemetryRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task CreateAsync(CreateTelemetryRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (string.IsNullOrWhiteSpace(dto.PestType))
        {
            throw new BadRequestException(
                "El tipo de plaga es obligatorio.");
        }

        if (dto.Confidence < 0 || dto.Confidence > 1)
        {
            throw new BadRequestException(
                "La confianza debe estar entre 0 y 1.");
        }

        if (dto.InferenceTimeMs < 0)
        {
            throw new BadRequestException(
                "El tiempo de inferencia no puede ser negativo.");
        }

        if (dto.InspectionCount < 0)
        {
            throw new BadRequestException(
                "La cantidad de inspecciones no puede ser negativa.");
        }

        var telemetry = new Telemetry
        {
            Timestamp = dto.Timestamp,

            PestType = dto.PestType.Trim(),

            Confidence = dto.Confidence,

            InferenceTimeMs = dto.InferenceTimeMs,

            InspectionCount = dto.InspectionCount,

            DeviceHash = dto.DeviceHash,

            SyncStatus = "Completed"
        };

        await _repository.AddAsync(telemetry);

        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<TelemetryDto>> GetAllAsync()
    {
        var telemetries =
            await _repository.GetAllAsync();

        return telemetries.Select(x => new TelemetryDto
        {
            Id = x.Id,
            Timestamp = x.Timestamp,
            PestType = x.PestType,
            Confidence = x.Confidence,
            InferenceTimeMs = x.InferenceTimeMs,
            InspectionCount = x.InspectionCount,
            DeviceHash = x.DeviceHash,
            SyncStatus = x.SyncStatus
        }).ToList();
    }

    public async Task<TelemetryDto> GetByIdAsync(Guid id)
    {
        var telemetry =await _repository.GetByIdAsync(id);

        if (telemetry is null)
        {
            throw new NotFoundException($"No existe una telemetría con Id {id}");
        }

        return new TelemetryDto
        {
            Id = telemetry.Id,
            Timestamp = telemetry.Timestamp,
            PestType = telemetry.PestType,
            Confidence = telemetry.Confidence,
            InferenceTimeMs = telemetry.InferenceTimeMs,
            InspectionCount = telemetry.InspectionCount,
            DeviceHash = telemetry.DeviceHash,
            SyncStatus = telemetry.SyncStatus
        };
    }
}
