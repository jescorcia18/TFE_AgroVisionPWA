using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class InferenceResultConfiguration
: IEntityTypeConfiguration<InferenceResult>
{
    public void Configure(
        EntityTypeBuilder<InferenceResult> builder)
    {
        builder.ToTable("inference_results");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
          .HasColumnName("id");

        builder.Property(x => x.ImageId)
           .HasColumnName("image_id");

        builder.Property(x => x.PredictedDiseaseId)
            .HasColumnName("predicted_disease_id");


        builder.Property(x => x.ModelName)
            .HasColumnName("model_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ModelVersion)
            .HasColumnName("model_version")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Confidence)
            .HasColumnName("confidence")
            .HasPrecision(5, 4);

        builder.Property(x => x.TopKJson)
            .HasColumnName("top_k_json");

        builder.Property(x => x.InferenceTimeMs)
            .HasColumnName("inference_time_ms");

        builder.Property(x => x.TfBackend)
            .HasColumnName("tf_backend")
            .HasMaxLength(50);

        builder.Property(x => x.DeviceMemoryGb)
            .HasColumnName("device_memory_gb")
            .HasPrecision(5, 2);

        builder.Property(x => x.Browser)
            .HasColumnName("browser")
            .HasMaxLength(100);

        builder.Property(x => x.BrowserVersion)
            .HasColumnName("browser_version")
            .HasMaxLength(50);

        builder.Property(x => x.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);

        builder.Property(x => x.Platform)
            .HasColumnName("platform")
            .HasMaxLength(100);

        builder.Property(x => x.OperatingSystem)
            .HasColumnName("operating_system")
            .HasMaxLength(100);

        builder.Property(x => x.TensorflowVersion)
            .HasColumnName("tensorflow_version")
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
           .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("IsDeleted");

        builder.HasOne(x => x.Image)
            .WithMany(x => x.InferenceResults)
            .HasForeignKey(x => x.ImageId);

        builder.HasOne(x => x.PredictedDisease)
            .WithMany(x => x.InferenceResults)
            .HasForeignKey(x => x.PredictedDiseaseId);
    }
}
