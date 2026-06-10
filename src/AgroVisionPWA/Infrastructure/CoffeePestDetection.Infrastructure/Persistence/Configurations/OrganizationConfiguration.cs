using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

/// <summary>
/// Clase para manejar el Fluent API
/// </summary>
public class OrganizationConfiguration
    : IEntityTypeConfiguration<Organization>
{
    public void Configure(
        EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Nit)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.type)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();


        builder.HasMany(x => x.Profiles)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId)
             .OnDelete(DeleteBehavior.Restrict); //para no borrar usuario accidentalmente si una organización se borra;
    }
}
