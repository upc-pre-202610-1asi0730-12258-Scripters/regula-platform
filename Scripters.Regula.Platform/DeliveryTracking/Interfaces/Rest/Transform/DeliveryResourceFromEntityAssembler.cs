using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Transform;

public static class DeliveryResourceFromEntityAssembler
{
    public static DeliveryResource ToResourceFromEntity(Delivery entity)
    {
        return new DeliveryResource(
            entity.Id,
            entity.Status.ToString().ToUpperInvariant(),
            entity.ResponsibleId,
            entity.Vehicle.Plate,
            entity.ItemCount,
            entity.ScheduledTime);
    }
}
