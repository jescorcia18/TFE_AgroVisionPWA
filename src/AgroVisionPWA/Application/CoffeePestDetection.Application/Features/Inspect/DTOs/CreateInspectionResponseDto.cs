using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Inspect.DTOs
{
    public class CreateInspectionResponseDto
    {
        public Guid Id { get; set; }

        public DateTime InspectionDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
