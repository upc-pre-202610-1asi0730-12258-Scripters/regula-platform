using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Repositories;

namespace Scripters.Regula.Platform.Alerts.Domain.Repositories;

public interface IAlertRepository : IBaseRepository<Alert>
{
    Task<IEnumerable<Alert>> FindByStatusAsync(EAlertStatus status, CancellationToken cancellationToken = default);
}
