using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.IA.DTOs;

public class FileResultDto
{
    public byte[] Content { get; set; }= [];

    public string ContentType { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
}