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

        builder.Property(x => x.FarmId)
           .HasColumnName("farm_id");

        builder.Property(x => x.AreaHectares);

        builder.Property(x => x.IsActive)
            .HasColumnName("isActived")
            .HasDefaultValue(true);

        builder.HasOne(x => x.Farm)
            .WithMany(x => x.Plots)
            .HasForeignKey(x => x.FarmId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_At");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_At");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isDeleted");
    }
}