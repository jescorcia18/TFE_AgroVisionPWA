
using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Profile> Profiles => Set<Profile>();

    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<Inspection> Inspections => Set<Inspection>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
