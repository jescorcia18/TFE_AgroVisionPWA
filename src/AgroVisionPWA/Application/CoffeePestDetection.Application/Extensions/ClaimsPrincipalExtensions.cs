using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
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
}
