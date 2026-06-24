using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class TelemetryConfiguration : IEntityTypeConfiguration<Telemetry>
{
    public void Configure(EntityTypeBuilder<Telemetry> builder)
    {
        builder.ToTable("telemetries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Timestamp)
            .HasColumnName("timestamp")
            .IsRequired();

        builder.Property(x => x.PestType)
            .HasColumnName("pest_type")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Confidence)
            .HasColumnName("confidence")
            .HasPrecision(5, 4)
            .IsRequired();

        builder.Property(x => x.InferenceTimeMs)
            .HasColumnName("inference_time_ms");

        builder.Property(x => x.InspectionCount)
            .HasColumnName("inspection_count");

        builder.Property(x => x.DeviceHash)
            .HasColumnName("device_hash")
            .HasMaxLength(200);

        builder.Property(x => x.SyncStatus)
            .HasColumnName("sync_status")
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_At");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_At");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isDeleted");
    }
}
