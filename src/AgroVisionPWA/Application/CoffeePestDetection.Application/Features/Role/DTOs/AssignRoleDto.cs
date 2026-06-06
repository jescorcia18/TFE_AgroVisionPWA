using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Features.Role.DTOs
{
    public class AssignRoleDto
    {
        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [Required]
        public string RolSolicitado { get; set; } = string.Empty; // Aquí llegará el Rol ej:"farmer"
    }
}
