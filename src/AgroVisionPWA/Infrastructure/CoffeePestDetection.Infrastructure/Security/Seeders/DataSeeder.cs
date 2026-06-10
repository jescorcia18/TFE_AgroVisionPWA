using CoffeePestDetection.Infrastructure.Persistence;


namespace CoffeePestDetection.Infrastructure.Security.Seeders;

public static class DataSeeder
{
    private static readonly Guid OrganizationId =
       Guid.Parse("11111111-1111-1111-1111-111111111111");

    public static async Task SeedAsync(ApplicationDbContext context)
    {

        //Crear Organización Demo

        await OrganizationSeeder
            .SeedAsync(context);

        await ProfileSeeder
            .SeedAsync(context);
    }
}
