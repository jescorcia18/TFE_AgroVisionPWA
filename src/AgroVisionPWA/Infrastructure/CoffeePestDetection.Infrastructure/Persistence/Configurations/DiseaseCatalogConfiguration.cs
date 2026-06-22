using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class DiseaseCatalogConfiguration
 : IEntityTypeConfiguration<DiseaseCatalog>
{
    public void Configure(
        EntityTypeBuilder<DiseaseCatalog> builder)
    {
        builder.ToTable("disease_catalog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.CommonName)
            .HasColumnName("common_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.CommonName)
           .HasColumnName("recommendation");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted");
    }
}
