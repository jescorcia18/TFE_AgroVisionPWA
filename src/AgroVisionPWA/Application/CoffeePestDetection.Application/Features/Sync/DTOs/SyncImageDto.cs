
namespace CoffeePestDetection.Application.Features.Sync.DTOs;

public class SyncImageDto
{
    public Guid Id { get; set; }

    public Guid InspectionId { get; set; }

    public string FileUri { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;

    public int Width { get; set; }

    public int Height { get; set; }

    public string DeviceId { get; set; } = string.Empty;
}
