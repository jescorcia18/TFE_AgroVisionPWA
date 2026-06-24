using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.IA.DTOs;

public class ModelVersionDto
{
    public string ModelName { get; set; } = string.Empty;

    public string Version { get; set; }  = string.Empty;

    public string ModelJsonPath { get; set; }  = string.Empty;

    public string WeightsPath { get; set; } = string.Empty;

    public string? Checksum { get; set; }

    public DateTime ReleasedAt { get; set; }
}
