using Scripters.Regula.Platform.InventoryManagement.Application.QueryServices;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Queries;
using Scripters.Regula.Platform.InventoryManagement.Domain.Repositories;

namespace Scripters.Regula.Platform.InventoryManagement.Application.Internal.QueryServices;

public class InventoryQueryService(IInventoryRepository inventoryRepository) : IInventoryQueryService
{
    public async Task<Inventory?> Handle(GetInventoryByIdQuery query, CancellationToken cancellationToken)
    {
        return await inventoryRepository.FindSummaryByIdAsync((int)query.InventoryId, cancellationToken);
    }
}
