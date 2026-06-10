using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Enums.Features.Auth;

public static class AuthEnum
{
    public enum Roles
    {
        [Description("farmer")]
        Farmer,

        [Description("admin")]
        Admin,

        [Description("inspector")]
        Inspector,

        [Description("technician")]
        Technician
    }

    public static AuthEnum.Roles ParseRole(string? role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return AuthEnum.Roles.Farmer;

        foreach (AuthEnum.Roles item in Enum.GetValues(typeof(AuthEnum.Roles)))
        {
            if (item.GetDescription()
                .Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                return item;
            }
        }

        throw new Exception("Rol inválido.");
    }
}
