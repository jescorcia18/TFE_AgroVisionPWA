using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.DiseaseCat.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services;

public class DiseaseCatalogService : IDiseaseCatalogService
{
    private readonly IDiseaseCatalogRepository _repository;

    private readonly ApplicationDbContext _context;

    public DiseaseCatalogService(IDiseaseCatalogRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IReadOnlyList<DiseaseCatalogDto>> GetAllAsync()
    {
        var diseases = await _repository.GetAllAsync();

        return diseases
            .Select(x => new DiseaseCatalogDto
            {
                Id = x.Id,
                CommonName = x.CommonName
            })
            .ToList();
    }

    public async Task<DiseaseCatalogDto> GetByIdAsync(Guid id)
    {
        var disease = await _repository.GetByIdAsync(id);

        if (disease is null)
        {
            throw new NotFoundException("La enfermedad no existe.");
        }

        return new DiseaseCatalogDto
        {
            Id = disease.Id,
            CommonName = disease.CommonName
        };
    }

    public async Task<DiseaseCatalogDto> CreateAsync(DiseaseCatalogRequestDto request)
    {
        ValidateRequest(request);

        var exists = await _repository.ExistsByNameAsync(request.CommonName.Trim());

        if (exists)
        {
            throw new BadRequestException("Ya existe una enfermedad con ese nombre.");
        }

        var disease =
            new DiseaseCatalog
            {
                Id = Guid.NewGuid(),
                CommonName = request.CommonName.Trim(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

        await _repository.AddAsync(disease);

        await _context.SaveChangesAsync();

        return new DiseaseCatalogDto
        {
            Id = disease.Id,
            CommonName = disease.CommonName
        };
    }

    public async Task UpdateAsync(Guid id, DiseaseCatalogRequestDto request)
    {
        ValidateRequest(request);

        var disease = await _repository.GetByIdAsync(id);

        if (disease is null)
        {
            throw new NotFoundException("La enfermedad no existe.");
        }

        var exists = await _repository
            .ExistsByNameAsync(request.CommonName.Trim(), id);

        if (exists)
        {
            throw new BadRequestException("Ya existe una enfermedad con ese nombre.");
        }

        disease.CommonName = request.CommonName.Trim();

        disease.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(disease);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var disease = await _repository.GetByIdAsync(id);

        if (disease is null)
        {
            throw new NotFoundException("La enfermedad no existe.");
        }

        await _repository.DeleteAsync(disease);

        await _context.SaveChangesAsync();
    }

    private static void ValidateRequest(
        DiseaseCatalogRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.CommonName))
        {
            throw new BadRequestException("El nombre común es obligatorio.");
        }

        if (request.CommonName.Length > 150)
        {
            throw new BadRequestException("El nombre común no puede superar los 150 caracteres.");
        }
    }
}
