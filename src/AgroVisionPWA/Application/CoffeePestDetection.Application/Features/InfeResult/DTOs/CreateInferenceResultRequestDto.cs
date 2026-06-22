using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.InfeResult.DTOs;

public class CreateInferenceResultRequestDto
{
    public Guid ImageId { get; set; }

    public Guid PredictedDiseaseId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public string ModelVersion { get; set; } = string.Empty;

    public decimal Confidence { get; set; }

    public string? TopKJson { get; set; }
}
