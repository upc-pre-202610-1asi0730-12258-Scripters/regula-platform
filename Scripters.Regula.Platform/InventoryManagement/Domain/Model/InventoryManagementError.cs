namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model;

public enum InventoryManagementError
{
    None,
    InventoryNotFound,
    InvalidInventoryType,
    InsufficientStock,
    InvalidProviderName,
    InvalidOutboundType,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
