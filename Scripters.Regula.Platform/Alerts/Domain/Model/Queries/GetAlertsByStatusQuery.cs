using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.Alerts.Domain.Model.Queries;

public record GetAlertsByStatusQuery(EAlertStatus Status);
