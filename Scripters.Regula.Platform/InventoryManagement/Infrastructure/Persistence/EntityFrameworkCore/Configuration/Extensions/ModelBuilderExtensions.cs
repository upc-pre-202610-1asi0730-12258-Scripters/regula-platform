using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly ValueConverter<ProviderName, string> ProviderNameConverter =
        new(v => v.Value, v => new ProviderName(v));

    private static readonly ValueConverter<ProfileId, long> ProfileIdConverter =
        new(v => v.Value, v => new ProfileId(v));

    private static readonly ValueConverter<Quantity, int> QuantityConverter =
        new(v => v.Value, v => new Quantity(v));

    private static readonly ValueConverter<OriginDestination, string> OriginDestinationConverter =
        new(v => v.Value, v => new OriginDestination(v));

    private static readonly ValueConverter<MovementReason, string> MovementReasonConverter =
        new(v => v.Value, v => new MovementReason(v));

    private static readonly ValueConverter<Observation, string> ObservationConverter =
        new(v => v.Value, v => new Observation(v));

    private static readonly ValueConverter<PreviousValue, string> PreviousValueConverter =
        new(v => v.Value, v => new PreviousValue(v));

    private static readonly ValueConverter<NewValue, string> NewValueConverter =
        new(v => v.Value, v => new NewValue(v));

    public static void ApplyInventoryManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Inventory>(entity =>
        {
            entity.ToTable("inventories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.OwnerProfileId)
                .HasConversion(ProfileIdConverter)
                .HasColumnName("owner_profile_id")
                .IsRequired();

            entity.Property(x => x.InventoryType)
                .HasConversion<string>()
                .HasColumnName("inventory_type")
                .HasMaxLength(20)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.OwnerProfileId,
                x.InventoryType
            }).IsUnique();

            entity.Ignore(x => x.StockSummary);
            entity.Ignore(x => x.MovementHistory);
            entity.Ignore(x => x.AuditHistory);
            entity.Ignore(x => x.ProviderCatalog);

            entity.HasMany(x => x.StockItems)
                .WithOne()
                .HasForeignKey("inventory_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Movements)
                .WithOne()
                .HasForeignKey("inventory_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.AuditLogs)
                .WithOne()
                .HasForeignKey("inventory_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Providers)
                .WithOne()
                .HasForeignKey("inventory_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<GasCylinderStock>(entity =>
        {
            entity.ToTable("gas_cylinder_stocks");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.CylinderType)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(x => x.Available)
                .HasConversion(QuantityConverter)
                .HasColumnName("available")
                .IsRequired();

            entity.Property(x => x.InTransit)
                .HasConversion(QuantityConverter)
                .HasColumnName("in_transit")
                .IsRequired();

            entity.Property(x => x.Observed)
                .HasConversion(QuantityConverter)
                .HasColumnName("observed")
                .IsRequired();

            entity.Property(x => x.OutOfService)
                .HasConversion(QuantityConverter)
                .HasColumnName("out_of_service")
                .IsRequired();
        });

        builder.Entity<Movement>(entity =>
        {
            entity.UseTptMappingStrategy();

            entity.ToTable("movements");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Timestamp)
                .IsRequired();

            entity.Property(x => x.MovementType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.CylinderType)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(x => x.Quantity)
                .HasConversion(QuantityConverter)
                .HasColumnName("quantity")
                .IsRequired();

            entity.Property(x => x.ProfileId)
                .HasConversion(ProfileIdConverter)
                .HasColumnName("profile_id")
                .IsRequired();
        });

        builder.Entity<CompanyMovement>(entity =>
        {
            entity.ToTable("company_movements");

            entity.Property(x => x.OriginDestination)
                .HasConversion(OriginDestinationConverter)
                .HasColumnName("origin_destination")
                .HasMaxLength(200);

            entity.Property(x => x.MovementReason)
                .HasConversion(MovementReasonConverter)
                .HasColumnName("movement_reason")
                .HasMaxLength(300);

            entity.Property(x => x.Observation)
                .HasConversion(ObservationConverter)
                .HasColumnName("observation")
                .HasMaxLength(500);
        });

        builder.Entity<DistributorMovement>(entity =>
        {
            entity.ToTable("distributor_movements");

            entity.Property(x => x.ProviderName)
                .HasConversion(ProviderNameConverter)
                .HasColumnName("provider_name")
                .HasMaxLength(100);
        });

        builder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("audit_logs");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.EntityName)
                .HasColumnName("entity_name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.EntityId)
                .IsRequired();

            entity.Property(x => x.OperationType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.PreviousValue)
                .HasConversion(PreviousValueConverter)
                .HasColumnName("previous_value")
                .HasMaxLength(2000);

            entity.Property(x => x.NewValue)
                .HasConversion(NewValueConverter)
                .HasColumnName("new_value")
                .HasMaxLength(2000);

            entity.Property(x => x.ProfileId)
                .HasConversion(ProfileIdConverter)
                .HasColumnName("profile_id")
                .IsRequired();
        });

        builder.Entity<Provider>(entity =>
        {
            entity.ToTable("providers");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.ProviderName)
                .HasConversion(ProviderNameConverter)
                .HasColumnName("provider_name")
                .HasMaxLength(100)
                .IsRequired();
        });
    }
}
