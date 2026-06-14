using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Transform;

public static class DeliveryLocationResourceFromEntityAssembler
{
    public static DeliveryLocationResource ToActiveLocationResource(DriverLocation entity)
    {
        return new DeliveryLocationResource(
            entity.DriverId,
            entity.Latitude,
            entity.Longitude,
            entity.LastUpdated,
            entity.Eta);
    }

    public static NoSignalLocationResource ToNoSignalLocationResource(DriverLocation entity)
    {
        return new NoSignalLocationResource(
            "NO_SIGNAL",
            new LastKnownPositionResource(entity.Latitude, entity.Longitude),
            entity.LastUpdated.ToString("HH:mm"));
    }
}
