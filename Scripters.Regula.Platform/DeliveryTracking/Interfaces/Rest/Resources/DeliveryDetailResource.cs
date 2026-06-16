namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

public record DeliveryDetailResource(
    int Id,
    string Status,
    DateTime ScheduledTime,
    DeliveryResponsibleResource Responsible,
    DeliveryVehicleResource Vehicle
);

public record DeliveryResponsibleResource(string Name);

public record DeliveryVehicleResource(string Plate, string Type, string Brand);
