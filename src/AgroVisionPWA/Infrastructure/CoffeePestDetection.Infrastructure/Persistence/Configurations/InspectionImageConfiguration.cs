using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class InspectionImageConfiguration : IEntityTypeConfiguration<InspectionImage>
{
    public void Configure(EntityTypeBuilder<InspectionImage> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(x => x.Id);

        builder
        .Property(x => x.FileUri)
        .HasMaxLength(int.MaxValue)
        .IsRequired();

        builder
        .Property(x => x.MimeType)
        .HasMaxLength(50)
        .IsRequired();

        builder
        .Property(x => x.DeviceId)
        .HasMaxLength(100)
        .IsRequired();

        builder
        .Property(x => x.CreatedAt)
        .IsRequired();

        builder
        .HasOne(x => x.Inspection)
        .WithMany(x => x.Images)
        .HasForeignKey(x => x.InspectionId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
