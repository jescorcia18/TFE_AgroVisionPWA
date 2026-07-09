using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
{
    public void Configure(EntityTypeBuilder<Observation> builder)
    {
        builder.ToTable("observations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.InspectionId)
            .HasColumnName("inspection_id");

        builder.Property(x => x.DiseaseId)
            .HasColumnName("disease_id");

        builder.Property(x => x.SeverityLevel)
            .HasColumnName("severity_level");

        builder.Property(x => x.IncidencePercent)
            .HasColumnName("incidence_percent")
            .HasPrecision(5, 2);

        builder.Property(x => x.SourceType)
            .HasColumnName("source_type")
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_At");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_At");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("IsDeleted");

        builder.HasOne(x => x.Inspection)
            .WithMany(x => x.Observations)
            .HasForeignKey(x => x.InspectionId);

        builder.HasOne(x => x.Disease)
            .WithMany(x => x.Observations)
            .HasForeignKey(x => x.DiseaseId);
    }
}
