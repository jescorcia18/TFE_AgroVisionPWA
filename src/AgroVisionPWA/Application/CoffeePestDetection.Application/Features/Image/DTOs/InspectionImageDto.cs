using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Image.DTOs;

public class InspectionImageDto
{
    public Guid Id { get; set; }

    public Guid InspectionId { get; set; }

    public string FileUri { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;

    public int Width { get; set; }

    public int Height { get; set; }

    public DateTime CreatedAt { get; set; }
}
