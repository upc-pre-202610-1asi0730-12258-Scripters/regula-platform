namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

public record DeliveryResource(
    int Id,
    string Status,
    int ResponsibleId,
    string VehiclePlate,
    int ItemCount,
    DateTime ScheduledTime
);
