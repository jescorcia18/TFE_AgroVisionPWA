using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations
{
    public class AiModelConfiguration : IEntityTypeConfiguration<AiModel>
    {
        public void Configure(EntityTypeBuilder<AiModel> builder)
        {
            builder.ToTable("ai_models");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.ModelName)
                .HasColumnName("model_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Version)
                .HasColumnName("version")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ModelJsonPath)
                .HasColumnName("download_url")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.WeightsPath)
               .HasColumnName("weightsPath")
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(x => x.Checksum)
                .HasColumnName("checksum")
                .HasMaxLength(200);

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            // BaseEntity

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();

            // Índice recomendado para consultas frecuentes

            builder.HasIndex(x => new
            {
                x.IsActive,
                x.IsDeleted
            })
            .HasDatabaseName("IX_ai_models_active");
        }
    }
}
