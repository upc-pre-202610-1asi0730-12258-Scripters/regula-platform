using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;

public class Alert : IAuditableEntity
{
    public int Id { get; private set; }

    public string Zone { get; private set; } = string.Empty;

    public double PpmLevel { get; private set; }

    public EAlertCriticality Criticality { get; private set; }

    public EAlertStatus Status { get; private set; }

    public string DetectedAt { get; private set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected Alert()
    {
    }

    public Alert(string zone, double ppmLevel, EAlertCriticality criticality, string detectedAt)
    {
        Zone = zone;
        PpmLevel = ppmLevel;
        Criticality = criticality;
        DetectedAt = detectedAt;
        Status = EAlertStatus.Pending;
    }
}
