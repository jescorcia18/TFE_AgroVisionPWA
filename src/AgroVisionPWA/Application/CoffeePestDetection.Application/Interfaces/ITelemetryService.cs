using CoffeePestDetection.Application.Features.Telemetry.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Interfaces;

public interface ITelemetryService
{
    Task CreateAsync( CreateTelemetryRequestDto dto);

    Task<IReadOnlyList<TelemetryDto>>GetAllAsync();

    Task<TelemetryDto> GetByIdAsync(Guid id);
}
