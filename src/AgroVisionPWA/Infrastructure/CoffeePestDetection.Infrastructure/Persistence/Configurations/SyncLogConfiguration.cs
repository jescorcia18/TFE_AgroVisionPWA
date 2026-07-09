using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class SyncLogConfiguration: IEntityTypeConfiguration<SyncLog>
{
    public void Configure(
        EntityTypeBuilder<SyncLog> builder)
    {
        builder.ToTable("sync_logs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeviceId)
            .HasColumnName("device_id")
            .HasMaxLength(100);

        builder.Property(x => x.SyncedEntities)
            .HasColumnName("synced_entities");

        builder.Property(x => x.SyncedInspections)
            .HasColumnName("synced_inspections");

        builder.Property(x => x.SyncedImages)
            .HasColumnName("synced_images");

        builder.Property(x => x.SyncedObservations)
            .HasColumnName("synced_observations");

        builder.Property(x => x.SyncedInferenceResults)
            .HasColumnName("synced_inference_results");

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasMaxLength(50);

        builder.Property(x => x.ErrorMessage)
            .HasColumnName("error_message");

        builder.Property(x => x.StartedAt)
            .HasColumnName("started_at");

        builder.Property(x => x.FinishedAt)
            .HasColumnName("finished_at");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted");

        builder.Property(x => x.ExceptionType)
            .HasColumnName("exception_type");

        builder.Property(x => x.ExecutionTimeMs)
            .HasColumnName("execution_time_ms");
    }
}
