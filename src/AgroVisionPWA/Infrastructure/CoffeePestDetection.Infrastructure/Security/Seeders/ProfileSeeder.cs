using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Auth;
using CoffeePestDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Security.Seeders;

public static class ProfileSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        var users = new List<Profile>
        {
            new()
            {
                Id = Guid.NewGuid(),

                FullName = "Administrador",

                Email = "admin@agrovision.co",

                Role = AuthEnum.Roles.Admin,

                PasswordHash =
                    PasswordHasher.Hash(
                        "admin2026"),

                OrganizationId =
                    OrganizationSeeder.AgroVisionId
            },

            new()
            {
                Id = Guid.NewGuid(),

                FullName = "Inspector Principal",

                Email =
                    "inspector@agrovision.co",

                Role = AuthEnum.Roles.Inspector,

                PasswordHash =
                    PasswordHasher.Hash(
                        "agro2026"),

                OrganizationId =
                    OrganizationSeeder.AgroVisionId
            },

            new()
            {
                Id = Guid.NewGuid(),

                FullName = "Juan Perez",

                Email =
                    "juan.perez@agrovision.co",

                Role = AuthEnum.Roles.Inspector,

                PasswordHash =
                    PasswordHasher.Hash(
                        "cafe2026"),

                OrganizationId =
                    OrganizationSeeder.AgroVisionId
            }
        };

        foreach (var user in users)
        {
            if (await context.Profiles
                .AnyAsync(x => x.Email == user.Email))
            {
                continue;
            }

            context.Profiles.Add(user);
        }

        await context.SaveChangesAsync();
    }
}
