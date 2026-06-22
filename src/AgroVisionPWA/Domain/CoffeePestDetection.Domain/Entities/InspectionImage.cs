
using CoffeePestDetection.Application.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeePestDetection.Domain.Entities;

public class InspectionImage
{
    public Guid Id { get; set; }

    [Column("inspection_id")]
    public Guid InspectionId { get; set; }

    [Column("file_uri")]
    public string FileUri { get; set; } = string.Empty;

    [Column("mime_type")]
    public string MimeType { get; set; } = string.Empty;

    public int Width { get; set; }

    public int Height { get; set; }

    [Column("device_id")]
    public string DeviceId { get; set; } = string.Empty;

    [Column("created_At")]
    public DateTime CreatedAt { get; set; }

    [Column("sync_status")]
    public DateTime SyncStatus { get; set; }

    [Column("inference_status")]
    public DateTime InferenceStatus { get; set; }

    /* Navigation */
    public Inspection Inspection { get; set; } = null!;

    public ICollection<InferenceResult> InferenceResults { get; set; } = new List<InferenceResult>();

}
