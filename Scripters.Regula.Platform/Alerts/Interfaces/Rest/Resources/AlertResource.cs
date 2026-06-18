namespace Scripters.Regula.Platform.Alerts.Interfaces.Rest.Resources;

public record AlertResource(
    int Id,
    string Zone,
    double PpmLevel,
    string Criticality,
    string Status,
    string DetectedAt
);
