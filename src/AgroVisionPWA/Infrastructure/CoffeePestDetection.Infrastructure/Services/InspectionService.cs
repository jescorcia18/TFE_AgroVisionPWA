using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Inspect.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Inspection;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;


namespace CoffeePestDetection.Infrastructure.Services;

public class InspectionService : IInspectionService
{
    private readonly IInspectionRepository _repo;
    private readonly IPlotRepository _plotRepo;
    private readonly ApplicationDbContext _context;

    public InspectionService(IInspectionRepository repo, ApplicationDbContext context, IPlotRepository plotRepo)
    {
        _repo = repo;
        _context = context;
        _plotRepo = plotRepo;
    }

    public async Task<CreateInspectionResponseDto> CreateAsync(Guid userId, Guid organizationId, CreateInspectionRequestDto request)
    {
        var inspection =
            new Inspection
            {
                Id = Guid.NewGuid(),
                InspectorId = userId,
                OrganizationId = organizationId,
                InspectionDate = request.InspectionDate,
                Status = InspectionEnum.Status.Pending.GetDescription(),
                CreatedAt = DateTime.UtcNow
            };

        await _repo.AddAsync(inspection);

        await _context.SaveChangesAsync();

        return
            new CreateInspectionResponseDto
            {
                Id = inspection.Id,
                InspectionDate = inspection.InspectionDate,
                Status = inspection.Status
            };
    }

    public async Task<CreateInspectionResponseDto?> GetByIdAsync(Guid id)
    {
        var inspection = await _repo.GetByIdAsync(id);

        if (inspection == null)
            throw new NotFoundException($"Inspección con ID: {id} no fue encontrada.");

        return new CreateInspectionResponseDto
        {
            Id = inspection.Id,
            InspectionDate = inspection.InspectionDate,
            Status = inspection.Status
        };
    }

    public async Task AssignPlotAsync(Guid inspectionId, Guid plotId, Guid organizationId)
    {
        var inspection = await _repo.GetByIdAsync(inspectionId);

        if (inspection is null)
        {
            throw new NotFoundException("La inspección no existe.");
        }

        var plot = await _plotRepo.GetByIdAsync(plotId);

        if (plot is null)
        {
            throw new NotFoundException("El lote no existe.");
        }

        if (plot.Farm.OrganizationId != organizationId)
        {
            throw new ForbiddenException("El lote no pertenece a la organización.");
        }

        inspection.PlotId = plot.Id;
        inspection.UpdatedAt= DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}
