using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.DeliveryTracking.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDeliveryTrackingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<DeliveryResponsible>().ToTable("delivery_responsibles");
        builder.Entity<DeliveryResponsible>().HasKey(r => r.Id);
        builder.Entity<DeliveryResponsible>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DeliveryResponsible>().Property(r => r.Name).IsRequired().HasMaxLength(100);

        builder.Entity<DeliveryResponsible>().HasData(
            new { Id = 1, Name = "Responsable de prueba" });

        builder.Entity<DeliveryVehicle>().ToTable("delivery_vehicles");
        builder.Entity<DeliveryVehicle>().HasKey(v => v.Id);
        builder.Entity<DeliveryVehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DeliveryVehicle>().Property(v => v.Plate).IsRequired().HasMaxLength(20);
        builder.Entity<DeliveryVehicle>().Property(v => v.Type).IsRequired().HasMaxLength(50);
        builder.Entity<DeliveryVehicle>().Property(v => v.Brand).IsRequired().HasMaxLength(50);

        builder.Entity<DeliveryVehicle>().HasData(
            new { Id = 1, Plate = "ABC-123", Type = "Van", Brand = "Toyota" });

        builder.Entity<Delivery>().ToTable("deliveries");
        builder.Entity<Delivery>().HasKey(d => d.Id);
        builder.Entity<Delivery>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Delivery>().Property(d => d.DriverId).IsRequired();
        builder.Entity<Delivery>().Property(d => d.ResponsibleId).IsRequired();
        builder.Entity<Delivery>().Property(d => d.VehicleId).IsRequired();
        builder.Entity<Delivery>().Property(d => d.ItemCount).IsRequired();
        builder.Entity<Delivery>().Property(d => d.ScheduledTime).IsRequired();
        builder.Entity<Delivery>().Property(d => d.DeliveredAt).HasMaxLength(5);

        builder.Entity<Delivery>()
            .Property(d => d.Status)
            .IsRequired()
            .HasConversion(
                status => status.ToString().ToUpperInvariant(),
                status => Enum.Parse<EDeliveryStatus>(status, true));

        builder.Entity<Delivery>()
            .HasOne(d => d.Responsible)
            .WithMany()
            .HasForeignKey(d => d.ResponsibleId);

        builder.Entity<Delivery>()
            .HasOne(d => d.Vehicle)
            .WithMany()
            .HasForeignKey(d => d.VehicleId);

        builder.Entity<Delivery>().HasData(
            new { Id = 1, DriverId = 101, ResponsibleId = 1, VehicleId = 1, ItemCount = 5, ScheduledTime = new DateTime(2026, 6, 16, 9, 0, 0), Status = EDeliveryStatus.Pending });

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
