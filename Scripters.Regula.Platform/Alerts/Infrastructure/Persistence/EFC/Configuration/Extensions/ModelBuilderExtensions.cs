using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>().ToTable("alerts");
        builder.Entity<Alert>().HasKey(a => a.Id);
        builder.Entity<Alert>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(a => a.Zone).IsRequired().HasMaxLength(100);
        builder.Entity<Alert>().Property(a => a.PpmLevel).IsRequired();
        builder.Entity<Alert>().Property(a => a.DetectedAt).IsRequired().HasMaxLength(50);

        builder.Entity<Alert>()
            .Property(a => a.Criticality)
            .IsRequired()
            .HasConversion(
                criticality => criticality.ToString().ToUpperInvariant(),
                criticality => Enum.Parse<EAlertCriticality>(criticality, true));

        builder.Entity<Alert>()
            .Property(a => a.Status)
            .IsRequired()
            .HasConversion(
                status => status.ToString().ToUpperInvariant(),
                status => Enum.Parse<EAlertStatus>(status, true));
    }
}
