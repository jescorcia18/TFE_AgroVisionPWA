using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.Auth.DTOs;
using CoffeePestDetection.Application.Features.Role.DTOs;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoffeePestDetection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpPost("roles")]
        [AllowAnonymous]
        public async Task<IActionResult> RolesList()
        {
            var listaRoles = await Task.Run(() => Enum.GetValues<AuthEnum.Roles>()
                .Select(rol => new RoleDto
                {
                    Id = (int)rol,
                    Key = rol.ToString(),       // Devuelve "Farmer"
                    Value = rol.GetValue()      // Devuelve "farmer" usando  extensión
                })
                .ToList()
                );

            return Ok(ApiResponse<List<RoleDto>>.Ok(listaRoles, "Obtener Roles exitosamente."));
        }

        [HttpPost("validate-role")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateAssignRole(AssignRoleDto role)
        {
            // LLAMADA DINÁMICA: Le indicamos explícitamente que busque en el Enum "Autorizacion"
            AuthEnum.Roles? rolConvertido = await Task.Run(() => EnumExtensions.GetFromDescription<AuthEnum.Roles>(role.RolSolicitado));

            if (rolConvertido == null)
            {
                var error = new List<string> { $"El rol '{role.RolSolicitado}' no existe en el sistema." };
                return NotFound(ApiResponse<string>.Fail("Validar Rol falló", error));
            }

            // Si pasa la validación, trabajas con el enum fuertemente tipado de forma segura
            AuthEnum.Roles rolFinal = rolConvertido.Value;

            //TODO: asignar role a usuario en caso que sea validado.
            //if (rolFinal == AuthEnum.Roles.Farmer)
            //{
            //    // Lógica específica si el usuario es Farmer
            //    return Ok(new
            //    {
            //        Mensaje = $"Rol 'farmer' (ID: {(int)rolFinal}) validado y asignado con éxito al usuario {modelo.UsuarioId}."
            //    });
            //}

            return Ok(ApiResponse<string>.Ok($"Rol '{rolFinal.GetValue()}' validado correctamente."));
        }
    }
}



