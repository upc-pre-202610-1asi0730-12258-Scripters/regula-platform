using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.InventoryManagement.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Scripters.Regula.Platform.InventoryManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InventoryRepository(AppDbContext context)
    : BaseRepository<Inventory>(context), IInventoryRepository
{
    public async Task<Inventory?> FindSummaryByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Inventory>()
            .Include(i => i.StockItems)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}
