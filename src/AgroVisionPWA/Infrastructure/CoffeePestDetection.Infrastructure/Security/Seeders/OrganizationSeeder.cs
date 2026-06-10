using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Security.Seeders;

public static class OrganizationSeeder
{
    public static readonly Guid AgroVisionId =
        Guid.Parse("11111111-1111-1111-1111-111111111111");

    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        if (await context.Organizations
            .AnyAsync(x => x.Id == AgroVisionId))
        {
            return;
        }

        var organization = new Organization
        {
            Id = AgroVisionId,

            Name = "AgroVision Demo",

            Nit = "900123456",

            IsActive = true,

            CreatedAt = DateTime.UtcNow
        };

        context.Organizations.Add(organization);

        await context.SaveChangesAsync();
    }
}
