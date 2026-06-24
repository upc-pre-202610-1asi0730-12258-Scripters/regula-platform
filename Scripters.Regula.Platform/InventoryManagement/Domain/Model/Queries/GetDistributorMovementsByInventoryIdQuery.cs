using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Queries;

public record GetDistributorMovementsByInventoryIdQuery(long InventoryId, EMovementType? MovementType);
