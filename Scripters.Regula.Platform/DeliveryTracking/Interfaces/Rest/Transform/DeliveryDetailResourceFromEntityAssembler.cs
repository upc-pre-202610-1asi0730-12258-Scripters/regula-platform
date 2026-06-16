using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Transform;

public static class DeliveryDetailResourceFromEntityAssembler
{
    public static DeliveryDetailResource ToResourceFromEntity(Delivery entity)
    {
        return new DeliveryDetailResource(
            entity.Id,
            entity.Status.ToString().ToUpperInvariant(),
            entity.ScheduledTime,
            new DeliveryResponsibleResource(entity.Responsible.Name),
            new DeliveryVehicleResource(entity.Vehicle.Plate, entity.Vehicle.Type, entity.Vehicle.Brand));
    }
}
