using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Auth.DTOs
{
    public class RegisterRequestDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Role { get; set; }

        public Guid? OrganizationId { get; set; }
    }
}
