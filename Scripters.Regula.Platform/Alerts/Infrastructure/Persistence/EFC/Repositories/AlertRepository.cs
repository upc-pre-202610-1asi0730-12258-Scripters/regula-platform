using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.Alerts.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Alerts.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Scripters.Regula.Platform.Alerts.Infrastructure.Persistence.EFC.Repositories;

public class AlertRepository(AppDbContext context)
    : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<IEnumerable<Alert>> FindByStatusAsync(
        EAlertStatus status,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<Alert>()
            .Where(a => a.Status == status)
            .ToListAsync(cancellationToken);
    }
}
