namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;

public record LastKnownPositionResource(
    double Latitude,
    double Longitude
);

public record NoSignalLocationResource(
    string Status,
    LastKnownPositionResource LastKnownPosition,
    string NoSignalSince
);
