using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeePestDetection.Application.Features.Image.DTOs;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;
using CoffeePestDetection.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Domain.Enums.Features.Sync;
using CoffeePestDetection.Domain.Enums;

namespace CoffeePestDetection.Infrastructure.Services;

public class InspectionImageService : IInspectionImageService
{
    private readonly IInspectionImageRepository _imageRepo;
    private readonly IInspectionRepository _inspectionRepo;
    private readonly ApplicationDbContext _context;

    private static readonly string[] AllowedMimeTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    public InspectionImageService(IInspectionImageRepository imageRepo, IInspectionRepository inspectionRepo, ApplicationDbContext context)
    {
        _imageRepo = imageRepo;
        _inspectionRepo = inspectionRepo;
        _context = context;
    }

    public async Task<InspectionImageDto> AddImageAsync( Guid organizationId, CreateInspectionImageRequestDto request)
    {
        var inspection = await _inspectionRepo.GetByIdAsync(request.InspectionId);

        if (inspection is null)
        {
            throw new NotFoundException("La inspección no existe.");
        }

        if (inspection.OrganizationId != organizationId)
        {
            throw new ForbiddenException("La inspección no pertenece a la organización.");
        }

        if (string.IsNullOrWhiteSpace(request.FileUri))
        {
            throw new ValidationException("La ruta de la imagen es obligatoria.");
        }

        if (!AllowedMimeTypes.Contains(request.MimeType, StringComparer.OrdinalIgnoreCase))
        {
            throw new ValidationException("Formato de imagen no permitido.");
        }

        if (request.Width < 300)
        {
            throw new ValidationException("El ancho mínimo permitido es 300 px.");
        }

        if (request.Height < 300)
        {
            throw new ValidationException("La altura mínima permitida es 300 px.");
        }

        if (request.Width > 6000 || request.Height > 6000)
        {
            throw new ValidationException("Las dimensiones exceden el máximo permitido.");
        }

        if (await _imageRepo.ExistsAsync(request.FileUri))
        {
            throw new ValidationException("La imagen ya existe.");
        }

        // Validar aspecto de la imagen
        //var ratio = (double)request.Width / request.Height;

        //if (ratio < 0.3 || ratio > 3.0)
        //{
        //    throw new ValidationException("La relación de aspecto de la imagen no es válida.");
        //}

        var image = new InspectionImage
        {
            Id = Guid.NewGuid(),
            InspectionId = request.InspectionId,
            FileUri = request.FileUri,
            MimeType = request.MimeType,
            Width = request.Width,
            Height = request.Height,
            DeviceId = request.DeviceId,
            CreatedAt = DateTime.UtcNow,
            SyncStatus = SyncLogEnum.Status.Success.GetDescription()
        };

        await _imageRepo.AddAsync(image);

        await _context.SaveChangesAsync();

        return new InspectionImageDto
        {
            Id = image.Id,
            InspectionId = image.InspectionId,
            FileUri = image.FileUri,
            MimeType = image.MimeType,
            Width = image.Width,
            Height = image.Height,
            CreatedAt = image.CreatedAt
        };
    }

    public async Task<IReadOnlyList<InspectionImageDto>> GetImagesAsync(Guid inspectionId, Guid organizationId)
    {
        var images = await _imageRepo.GetByInspectionAsync(inspectionId);

        return images.Select(x => new InspectionImageDto
        {
            Id = x.Id,
            InspectionId = x.InspectionId,
            FileUri = x.FileUri,
            MimeType = x.MimeType,
            Width = x.Width,
            Height = x.Height,
            CreatedAt = x.CreatedAt
        }).ToList();
    }
}
