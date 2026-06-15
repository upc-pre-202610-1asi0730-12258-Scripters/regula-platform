using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Queries;

namespace Scripters.Regula.Platform.InventoryManagement.Application.QueryServices;

public interface IInventoryQueryService
{
    Task<Inventory?> Handle(GetInventoryByIdQuery query, CancellationToken cancellationToken);
}
