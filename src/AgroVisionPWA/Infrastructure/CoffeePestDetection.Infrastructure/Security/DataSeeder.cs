using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Security;

public static class DataSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        if(await context.Profiles.AnyAsync(x => x.Email == "admin@coffee.com"))
            return;

        var user = new Profile
        {
            Id = Guid.NewGuid(),
            FullName = "Administrador",
            Email = "admin@coffee.com",
            Role = "admin",
            OrganizationId = new Guid("11111111-1111-1111-1111-111111111111"),
            PasswordHash = PasswordHasher.Hash("Admin123*")
        };

        context.Profiles.Add(user);

        await context.SaveChangesAsync();
    }
}
