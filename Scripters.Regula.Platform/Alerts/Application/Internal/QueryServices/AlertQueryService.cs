using Scripters.Regula.Platform.Alerts.Application.QueryServices;
using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Domain.Model.Queries;
using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Alerts.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.Alerts.Application.Internal.QueryServices;

public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    private static readonly Dictionary<EAlertCriticality, int> CriticalityOrder = new()
    {
        { EAlertCriticality.High, 0 },
        { EAlertCriticality.Medium, 1 },
        { EAlertCriticality.Low, 2 }
    };

    public async Task<Result<IEnumerable<Alert>>> Handle(
        GetAlertsByStatusQuery query,
        CancellationToken cancellationToken = default)
    {
        var alerts = await alertRepository.FindByStatusAsync(query.Status, cancellationToken);

        var ordered = alerts.OrderBy(a => CriticalityOrder[a.Criticality]);

        return Result<IEnumerable<Alert>>.Success(ordered);
    }
}
