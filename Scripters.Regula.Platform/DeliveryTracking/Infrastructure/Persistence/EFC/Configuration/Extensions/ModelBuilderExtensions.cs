using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;

namespace Scripters.Regula.Platform.DeliveryTracking.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDeliveryTrackingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Delivery>().ToTable("deliveries");
        builder.Entity<Delivery>().HasKey(d => d.Id);
        builder.Entity<Delivery>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Delivery>().Property(d => d.DriverId).IsRequired();

        builder.Entity<Delivery>().HasData(
            new { Id = 1, DriverId = 101 });

        builder.Entity<DriverLocation>().ToTable("driver_locations");
        builder.Entity<DriverLocation>().HasKey(l => l.Id);
        builder.Entity<DriverLocation>().Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DriverLocation>().Property(l => l.DriverId).IsRequired();
        builder.Entity<DriverLocation>().Property(l => l.Latitude).IsRequired();
        builder.Entity<DriverLocation>().Property(l => l.Longitude).IsRequired();
        builder.Entity<DriverLocation>().Property(l => l.LastUpdated).IsRequired();

        builder.Entity<DriverLocation>()
            .HasOne(l => l.Delivery)
            .WithMany()
            .HasForeignKey(l => l.DeliveryId);
    }
}
