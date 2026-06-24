using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Application.Features.Sync.DTOs;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums.Features.Sync;
using CoffeePestDetection.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Services;

public class SyncService : ISyncService
{
    private readonly ApplicationDbContext _context;

    private readonly IInspectionRepository _inspectionRepository;

    private readonly IInspectionImageRepository _imageRepository;

    private readonly IObservationRepository _observationRepository;

    private readonly IInferenceResultRepository _inferenceRepository;

    private readonly ISyncLogRepository _syncLogRepository;

    private readonly ITelemetryRepository _telemetryRepository;

    public SyncService(
        ApplicationDbContext context,
        IInspectionRepository inspectionRepository,
        IInspectionImageRepository imageRepository,
        IObservationRepository observationRepository,
        IInferenceResultRepository inferenceRepository,
        ISyncLogRepository syncLogRepository,
        ITelemetryRepository telemetryRepository)
    {
        _context = context;
        _inspectionRepository = inspectionRepository;
        _imageRepository = imageRepository;
        _observationRepository = observationRepository;
        _inferenceRepository = inferenceRepository;
        _syncLogRepository = syncLogRepository;
        _telemetryRepository = telemetryRepository;
    }

    public async Task<SyncBulkResponseDto> SyncBulkAsync(SyncBulkRequestDto request)
    {
        var strategy =_context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            var syncLog = new SyncLog
            {
                Id = Guid.NewGuid(),
                DeviceId = request.DeviceId,
                Status = SyncLogEnum.Status.Pending.ToString(),
                StartedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _syncLogRepository.AddAsync(syncLog);

            try
            {
                await _context.SaveChangesAsync();

                var syncedInspections = await SyncInspections(request.Inspections);

                var syncedImages = await SyncImages(request.Images);

                var syncedObservations = await SyncObservations(request.Observations);

                var syncedInferenceResults = await SyncInferenceResults(request.InferenceResults);

                foreach (var dto in request.Telemetries)
                {
                    var exists = await _telemetryRepository.GetByIdAsync(dto.Id);

                    if (exists is not null)
                        continue;

                    await _telemetryRepository.AddAsync(
                        new Telemetry
                        {
                            Id = dto.Id,
                            Timestamp = dto.Timestamp,
                            PestType = dto.PestType,
                            Confidence = dto.Confidence,
                            InferenceTimeMs = dto.InferenceTimeMs,
                            InspectionCount = dto.InspectionCount,
                            DeviceHash = dto.DeviceHash,
                            SyncStatus = "Completed"
                        });
                }

                await _context.SaveChangesAsync();

                syncLog.Status = SyncLogEnum.Status.Success.ToString();
                syncLog.SyncedEntities = syncedInspections + syncedImages + syncedObservations + syncedInferenceResults;
                syncLog.SyncedInspections = syncedInspections;
                syncLog.SyncedImages = syncedImages;
                syncLog.SyncedObservations = syncedObservations;
                syncLog.SyncedInferenceResults = syncedInferenceResults;
                syncLog.FinishedAt = DateTime.UtcNow;
                await transaction.CommitAsync();

                return new SyncBulkResponseDto
                {
                    Message =
                        "Sincronización masiva completada",

                    SyncedEntities =
                        syncedInspections +
                        syncedImages +
                        syncedObservations +
                        syncedInferenceResults,

                    SyncedInspections = syncedInspections,
                    SyncedImages = syncedImages,
                    SyncedObservations = syncedObservations,
                    SyncedInferenceResults = syncedInferenceResults,
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                syncLog.Status = "Failed";
                syncLog.ErrorMessage = ex.Message;
                syncLog.FinishedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public async Task<List<SyncLogDto>> GetLogsAsync()
    {
        var logs = await _syncLogRepository.GetAllAsync();

        return logs
            .Select(x => new SyncLogDto
            {
                Id = x.Id,
                DeviceId = x.DeviceId,
                SyncedEntities = x.SyncedEntities,
                SyncedInspections = x.SyncedInspections,
                SyncedImages = x.SyncedImages,
                SyncedObservations = x.SyncedObservations,
                SyncedInferenceResults = x.SyncedInferenceResults,
                Status = x.Status,
                ErrorMessage = x.ErrorMessage,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt
            })
            .ToList();
    }

    public async Task<List<SyncLogDto>?> GetLogsByDeviceAsync(string deviceId)
    {
        var logs = await _syncLogRepository.GetByDeviceAsync(deviceId);

        if (logs == null || !logs.Any())
            throw new NotFoundException ($"No se encontraron Logs para el DeviceID: {deviceId}");

        return logs.Select(MapToDto).ToList();
    }

    public async Task<SyncLogDto?> GetLastLogByDeviceAsync(string deviceId)
    {
        var log = await _syncLogRepository.GetLastByDeviceAsync(deviceId);

        if (log is null)
        {
            throw new NotFoundException($"No se encontraron ultimos Logs para el DeviceID: {deviceId}");
        }

        return MapToDto(log);
    }
    private async Task<int> SyncInspections(List<SyncInspectionDto> inspections)
    {
        int count = 0;

        foreach (var item in inspections)
        {
            if (await _inspectionRepository.ExistsAsync(item.Id))
            {
                continue;
            }

            await _context.Inspections.AddAsync(new Inspection
            {
                Id = item.Id,
                PlotId = item.PlotId,
                InspectorId = item.InspectorId,
                InspectionDate = item.InspectionDate,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            });

            count++;
        }

        return count;
    }

    private async Task<int> SyncImages(List<SyncImageDto> images)
    {
        int count = 0;

        foreach (var item in images)
        {
            if (await _imageRepository.ExistsAsync(item.Id))
            {
                continue;
            }

            await _context.InspectionImages.AddAsync(
                new InspectionImage
                {
                    Id = item.Id,
                    InspectionId = item.InspectionId,
                    FileUri = item.FileUri,
                    MimeType = item.MimeType,
                    Width = item.Width,
                    Height = item.Height,
                    DeviceId = item.DeviceId,
                    CreatedAt = DateTime.UtcNow
                    //IsDeleted = false
                });

            count++;
        }

        return count;
    }

    private async Task<int> SyncObservations(List<SyncObservationDto> observations)
    {
        int count = 0;

        foreach (var item in observations)
        {
            if (await _observationRepository.ExistsAsync(item.Id))
            {
                continue;
            }

            await _context.Observations.AddAsync(
                new Observation
                {
                    Id = item.Id,
                    InspectionId = item.InspectionId,
                    DiseaseId = item.DiseaseId,
                    SeverityLevel = item.SeverityLevel,
                    IncidencePercent = item.IncidencePercent,
                    SourceType = item.SourceType,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                });

            count++;
        }

        return count;
    }

    private async Task<int> SyncInferenceResults(List<SyncInferenceResultDto> results)
    {
        int count = 0;

        foreach (var item in results)
        {
            if (await _inferenceRepository.ExistsAsync(item.Id))
            {
                continue;
            }

            await _context.InferenceResults
                .AddAsync(
                    new InferenceResult
                    {
                        Id = item.Id,
                        ImageId = item.ImageId,
                        PredictedDiseaseId = item.PredictedDiseaseId,
                        ModelName = item.ModelName,
                        ModelVersion = item.ModelVersion,
                        Confidence = item.Confidence,
                        TopKJson = item.TopKJson,
                        InferenceTimeMs = item.InferenceTimeMs,
                        TfBackend = item.TfBackend,
                        DeviceMemoryGb = item.DeviceMemoryGb,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    });

            count++;
        }

        return count;
    }

    private static SyncLogDto MapToDto(SyncLog x)
    {
        return new SyncLogDto
        {
            Id = x.Id,
            DeviceId = x.DeviceId,
            SyncedEntities = x.SyncedEntities,
            SyncedInspections = x.SyncedInspections,
            SyncedImages = x.SyncedImages,
            SyncedObservations = x.SyncedObservations,
            SyncedInferenceResults = x.SyncedInferenceResults,
            Status = x.Status,
            ErrorMessage = x.ErrorMessage,
            StartedAt = x.StartedAt,
            FinishedAt = x.FinishedAt
        };
    }
}
