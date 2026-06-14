using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;

public class Delivery : IAuditableEntity
{
    public int Id { get; private set; }

    public int DriverId { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected Delivery()
    {
    }

    public Delivery(int driverId)
    {
        DriverId = driverId;
    }
}
