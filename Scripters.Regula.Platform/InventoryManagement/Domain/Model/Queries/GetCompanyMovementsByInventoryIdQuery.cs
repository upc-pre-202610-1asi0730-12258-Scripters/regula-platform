using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Queries;

public record GetCompanyMovementsByInventoryIdQuery(long InventoryId, EMovementType? MovementType);
