using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Extensions;

/// <summary>
/// Extension Method que devuelve los datos del token desde el claims
/// </summary>
public static class ClaimsPrincipalExtensions
{
    // <summary>
    /// Obtiene el Id del usuario autenticado.
    /// </summary>
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value =
            user.FindFirst(
                ClaimTypes.NameIdentifier)
            ?.Value;

        if (string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException();

        return Guid.Parse(value);
    }

    /// <summary>
    /// Obtiene la organización del usuario autenticado.
    /// </summary>
    public static Guid GetOrganizationId(this ClaimsPrincipal user)
    {
        var organizationId = user.FindFirst("organizationId")?.Value;

        if (string.IsNullOrEmpty(organizationId))
        {
            throw new UnauthorizedAccessException(
                "OrganizationId no encontrado.");
        }

        return Guid.Parse(
            organizationId);
    }

    /// <summary>
    /// Obtiene el correo electrónico del usuario.
    /// </summary>
    public static string GetEmail(
        this ClaimsPrincipal user)
    {
        var email =
            user.FindFirst(
                ClaimTypes.Email)
            ?.Value;

        if (string.IsNullOrWhiteSpace(email))
            throw new UnauthorizedAccessException(
                "Email no encontrado en el token.");

        return email;
    }

    /// <summary>
    /// Obtiene el rol del usuario.
    /// </summary>
    public static string GetRole(
        this ClaimsPrincipal user)
    {
        var role =
            user.FindFirst(
                ClaimTypes.Role)
            ?.Value;

        if (string.IsNullOrWhiteSpace(role))
            throw new UnauthorizedAccessException(
                "Rol no encontrado en el token.");

        return role;
    }

    /// <summary>
    /// Indica si el usuario tiene un rol determinado.
    /// </summary>
    public static bool HasRole(this ClaimsPrincipal user,string role)
    {
        return string.Equals(
            user.GetRole(),
            role,
            StringComparison.OrdinalIgnoreCase);
    }
}
