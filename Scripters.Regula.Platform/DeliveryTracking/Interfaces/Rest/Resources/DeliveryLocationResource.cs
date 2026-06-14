namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

public record DeliveryLocationResource(
    int DriverId,
    double Latitude,
    double Longitude,
    DateTime LastUpdated,
    DateTime? Eta
);
