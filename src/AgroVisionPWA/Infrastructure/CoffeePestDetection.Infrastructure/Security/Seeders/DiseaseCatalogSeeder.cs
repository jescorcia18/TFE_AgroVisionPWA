using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Security.Seeders;

public static class DiseaseCatalogSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        if (await context.DiseaseCatalogs.AnyAsync())
            return;

        context.DiseaseCatalogs.AddRange(
            new DiseaseCatalog
            {
                Id = Guid.Parse(
                    "11111111-1111-1111-1111-111111111111"),
                CommonName = "Broca del Café",
                Recommendation = "Inspeccionar frutos afectados y aplicar controles integrados para reducir la infestación.",
                CreatedAt = DateTime.UtcNow
            },
            new DiseaseCatalog
            {
                Id = Guid.Parse(
                    "22222222-2222-2222-2222-222222222222"),
                CommonName = "Roya del Cafeto",
                Recommendation= "Realizar monitoreo frecuente y aplicar tratamiento fungicida según recomendación técnica.",
                CreatedAt = DateTime.UtcNow
            },
            new DiseaseCatalog
            {
                Id = Guid.Parse(
                    "33333333-3333-3333-3333-333333333333"),
                CommonName = "Minador de Hoja",
                Recommendation= "Realizar seguimiento de hojas afectadas y evaluar medidas de control biológico.",
                CreatedAt = DateTime.UtcNow
            }
        );

        await context.SaveChangesAsync();
    }
}
