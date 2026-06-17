using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class PlotConfiguration : IEntityTypeConfiguration<Plot>
{
    public void Configure(EntityTypeBuilder<Plot> builder)
    {
        builder.ToTable("Plots");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.AreaHectares);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(x => x.Farm)
            .WithMany(x => x.Plots)
            .HasForeignKey(x => x.FarmId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}