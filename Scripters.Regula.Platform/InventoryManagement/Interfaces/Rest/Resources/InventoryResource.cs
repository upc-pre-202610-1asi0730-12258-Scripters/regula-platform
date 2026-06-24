namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Resources;

public record InventoryResource(
    int Id,
    long OwnerProfileId,
    string InventoryType,
    int TotalAvailable);

public record InventoryStockItem(
    int Id,
    string CylinderType,
    int Available,
    int InTransit,
    int Observed,
    int OutOfService);

public record InventoryCompanyMovementItem(
    int Id,
    DateTime Timestamp,
    string MovementType,
    string CylinderType,
    int Quantity,
    long ProfileId,
    string ProviderName,
    string Destination,
    string MovementReason,
    string Observation);

public record InventoryDistributorMovementItem(
    int Id,
    DateTime Timestamp,
    string MovementType,
    string CylinderType,
    int Quantity,
    long ProfileId,
    string ProviderName,
    string? OutboundType);
