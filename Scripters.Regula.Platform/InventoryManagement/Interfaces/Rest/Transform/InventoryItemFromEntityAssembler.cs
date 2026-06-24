using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Transform;

public static class InventoryItemFromEntityAssembler
{
    public static InventoryCompanyMovementItem ToCompanyMovementItem(CompanyMovement movement) =>
        new(
            movement.Id,
            movement.Timestamp,
            movement.MovementType.ToString(),
            movement.CylinderType.ToString(),
            movement.Quantity.Value,
            movement.ProfileId.Value,
            movement.ProviderName.Value,
            movement.Destination.Value,
            movement.MovementReason.Value,
            movement.Observation.Value);

    public static InventoryDistributorMovementItem ToDistributorMovementItem(DistributorMovement movement) =>
        new(
            movement.Id,
            movement.Timestamp,
            movement.MovementType.ToString(),
            movement.CylinderType.ToString(),
            movement.Quantity.Value,
            movement.ProfileId.Value,
            movement.ProviderName.Value,
            movement.OutboundType?.ToString());

    public static InventoryStockItem ToStockItem(GasCylinderStock stock) =>
        new(
            stock.Id,
            stock.CylinderType.ToString(),
            stock.Available.Value,
            stock.InTransit.Value,
            stock.Observed.Value,
            stock.OutOfService.Value);
}
