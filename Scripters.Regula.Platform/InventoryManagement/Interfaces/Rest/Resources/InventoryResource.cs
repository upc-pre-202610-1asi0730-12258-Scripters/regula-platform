namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Resources;

public record InventoryResource(
    int Id,
    long OwnerProfileId,
    string InventoryType,
    int TotalAvailable);
