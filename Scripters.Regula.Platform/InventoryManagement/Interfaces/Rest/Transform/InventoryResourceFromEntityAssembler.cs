using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Transform;

public static class InventoryResourceFromEntityAssembler
{
    public static InventoryResource ToResourceFromEntity(Inventory entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new InventoryResource(
            entity.Id,
            entity.OwnerProfileId.Value,
            entity.InventoryType.ToString(),
            entity.StockSummary.TotalAvailable());
    }
}
