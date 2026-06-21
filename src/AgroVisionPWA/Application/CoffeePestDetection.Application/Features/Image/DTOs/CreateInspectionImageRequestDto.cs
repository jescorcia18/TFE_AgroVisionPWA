
using System.ComponentModel.DataAnnotations;

namespace CoffeePestDetection.Application.Features.Image.DTOs;

public class CreateInspectionImageRequestDto
{
    [Required]
    public Guid InspectionId { get; set; }

    [Required]
    [MaxLength(500)]
    public string FileUri { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string MimeType { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Width { get; set; }

    [Range(1, 10000)]
    public int Height { get; set; }

    [Required]
    [MaxLength(100)]
    [RegularExpression(@"^[A-Za-z0-9\-_]+$")]
    public string DeviceId { get; set; } = string.Empty;
}
