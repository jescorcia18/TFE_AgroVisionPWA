
using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Plot> Plots => Set<Plot>();
    public DbSet<InspectionImage> InspectionImages => Set<InspectionImage>();
    public DbSet<DiseaseCatalog> DiseaseCatalogs => Set<DiseaseCatalog>();
    public DbSet<InferenceResult> InferenceResults => Set<InferenceResult>();
    public DbSet<Observation> Observations { get; set; } = null!;
    public DbSet<SyncLog> SyncLogs { get; set; } = null!;
    public DbSet<AiModel> AiModels { get; set; } = null!;
    public DbSet<Telemetry> Telemetries { get; set; }= null!;


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
