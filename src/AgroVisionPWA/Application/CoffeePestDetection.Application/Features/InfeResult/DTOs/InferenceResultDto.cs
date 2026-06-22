using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.InfeResult.DTOs
{
    public class InferenceResultDto
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Guid PredictedDiseaseId { get; set; }

        public string DiseaseName { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public string ModelVersion { get; set; } = string.Empty;

        public decimal Confidence { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Recommendation { get; set; }= string.Empty;
    }
}
