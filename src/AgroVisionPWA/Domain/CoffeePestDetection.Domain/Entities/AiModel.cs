using CoffeePestDetection.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities;

public class AiModel : BaseEntity
{
    public string ModelName { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;

    public string ModelJsonPath { get; set; } = string.Empty;

    public string WeightsPath { get; set; } = string.Empty;

    public string? Checksum { get; set; }

    public bool IsActive { get; set; }
}
