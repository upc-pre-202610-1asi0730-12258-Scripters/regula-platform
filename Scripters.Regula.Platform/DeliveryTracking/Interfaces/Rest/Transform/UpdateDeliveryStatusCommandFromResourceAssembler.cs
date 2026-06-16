using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Commands;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Transform;

public static class UpdateDeliveryStatusCommandFromResourceAssembler
{
    public static UpdateDeliveryStatusCommand ToCommandFromResource(int deliveryId, UpdateDeliveryStatusResource resource, EDeliveryStatus status)
    {
        return new UpdateDeliveryStatusCommand(deliveryId, status, resource.DeliveredAt);
    }
}
