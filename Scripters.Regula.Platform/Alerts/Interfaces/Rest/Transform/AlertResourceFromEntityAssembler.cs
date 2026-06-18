using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.Alerts.Interfaces.Rest.Transform;

public static class AlertResourceFromEntityAssembler
{
    public static AlertResource ToResourceFromEntity(Alert entity)
    {
        return new AlertResource(
            entity.Id,
            entity.Zone,
            entity.PpmLevel,
            entity.Criticality.ToString().ToUpperInvariant(),
            entity.Status.ToString().ToUpperInvariant(),
            entity.DetectedAt);
    }
}
