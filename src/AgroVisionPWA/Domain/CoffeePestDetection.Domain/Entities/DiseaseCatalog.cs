
using CoffeePestDetection.Application.Commons;

namespace CoffeePestDetection.Domain.Entities;

public class DiseaseCatalog : BaseEntity
{

    public string CommonName { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public ICollection<InferenceResult> InferenceResults { get; set; } = new List<InferenceResult>();
    public ICollection<Observation> Observations { get; set; } = new List<Observation>();
}
