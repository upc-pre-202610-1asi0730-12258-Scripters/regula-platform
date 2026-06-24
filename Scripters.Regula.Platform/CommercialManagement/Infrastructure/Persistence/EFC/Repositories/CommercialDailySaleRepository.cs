using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Scripters.Regula.Platform.CommercialManagement.Infrastructure.Persistence.EFC.Repositories;

public class CommercialDailySaleRepository(AppDbContext context)
    : BaseRepository<CommercialDailySale>(context), ICommercialDailySaleRepository
{
    public async Task<IEnumerable<CommercialDailySale>> ListOrderedByCreatedAtDescendingAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.CommercialDailySales
            .OrderByDescending(sale => sale.CreatedAt)
            .ThenByDescending(sale => sale.Id)
            .ToListAsync(cancellationToken);
    }
}