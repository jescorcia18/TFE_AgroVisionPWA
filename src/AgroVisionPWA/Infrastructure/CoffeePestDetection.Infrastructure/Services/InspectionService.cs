using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Inspect.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Inspection;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;


namespace CoffeePestDetection.Infrastructure.Services;

public class InspectionService : IInspectionService
{
    private readonly IInspectionRepository _repo;
    private readonly ApplicationDbContext _context;

    public InspectionService(IInspectionRepository repo, ApplicationDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<CreateInspectionResponseDto> CreateAsync(Guid userId, CreateInspectionRequestDto request)
    {
        var inspection =
            new Inspection
            {
                Id = Guid.NewGuid(),
                InspectorId = userId,
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
}
