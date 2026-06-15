using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;

public class DriverLocation : IAuditableEntity
{
    private static readonly TimeSpan ActiveSignalThreshold = TimeSpan.FromMinutes(5);

    public int Id { get; private set; }

    public int DeliveryId { get; private set; }

    public Delivery Delivery { get; private set; } = null!;

    public int DriverId { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public DateTime LastUpdated { get; private set; }

    public DateTime? Eta { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected DriverLocation()
    {
    }

    public DriverLocation(int deliveryId, int driverId, double latitude, double longitude, DateTime lastUpdated, DateTime? eta = null)
    {
        DeliveryId = deliveryId;
        DriverId = driverId;
        Latitude = latitude;
        Longitude = longitude;
        LastUpdated = lastUpdated;
        Eta = eta;
    }

    public EGpsSignalStatus GetSignalStatus()
    {
        var elapsed = DateTime.UtcNow - LastUpdated;
        return elapsed <= ActiveSignalThreshold ? EGpsSignalStatus.Active : EGpsSignalStatus.NoSignal;
    }

    public bool HasActiveSignal() => GetSignalStatus() == EGpsSignalStatus.Active;
}
