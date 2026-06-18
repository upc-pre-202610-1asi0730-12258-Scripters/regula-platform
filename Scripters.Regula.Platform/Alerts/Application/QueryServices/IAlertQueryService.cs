using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Domain.Model.Queries;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.Alerts.Application.QueryServices;

public interface IAlertQueryService
{
    Task<Result<IEnumerable<Alert>>> Handle(GetAlertsByStatusQuery query, CancellationToken cancellationToken = default);
}
