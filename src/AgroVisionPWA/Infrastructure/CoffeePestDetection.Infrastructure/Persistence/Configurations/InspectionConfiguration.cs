using CoffeePestDetection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Configurations;

public class InspectionConfiguration :
    IEntityTypeConfiguration<Inspection>
{
    public void Configure(
        EntityTypeBuilder<Inspection> builder)
    {
        builder.ToTable("Inspections");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InspectionDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Inspector)
            .WithMany(x => x.Inspections)
            .HasForeignKey(x => x.InspectorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Inspections)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Plot)
            .WithMany(x => x.Inspections)
            .HasForeignKey(x => x.PlotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
