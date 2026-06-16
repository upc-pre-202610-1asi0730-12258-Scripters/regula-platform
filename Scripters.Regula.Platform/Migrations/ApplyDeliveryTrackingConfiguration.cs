using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;

namespace Scripters.Regula.Platform.Deliv1eryTracking.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static partial class ModelBuilderExtensions
{
    public static void ApplyDeliveryTrackingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Delivery>(entity =>
        {
            entity.ToTable("deliveries");

            entity.HasKey(d => d.Id)
                .HasName("p_k_deliveries");

            entity.Property(d => d.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(d => d.DriverId)
                .IsRequired();

            entity.HasData(
                new { Id = 1, DriverId = 101 }
            );
        });

        builder.Entity<DriverLocation>(entity =>
        {
            entity.ToTable("driver_locations");

            entity.HasKey(l => l.Id)
                .HasName("p_k_driver_locations");

            entity.Property(l => l.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(l => l.DriverId)
                .IsRequired();

            entity.Property(l => l.Latitude)
                .IsRequired();

            entity.Property(l => l.Longitude)
                .IsRequired();

            entity.Property(l => l.LastUpdated)
                .IsRequired();

            // FK index (recomendado como en tu otro bounded context)
            entity.HasIndex(l => l.DeliveryId)
                .HasDatabaseName("i_x_driver_locations_delivery_id");

            entity.HasOne(l => l.Delivery)
                .WithMany()
                .HasForeignKey(l => l.DeliveryId)
                .HasConstraintName("f_k_driver_locations_deliveries_delivery_id")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}