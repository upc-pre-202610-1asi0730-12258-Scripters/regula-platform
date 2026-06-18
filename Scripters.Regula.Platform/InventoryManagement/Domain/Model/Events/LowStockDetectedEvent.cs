using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Events;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;

public class LowStockDetectedEvent(ECylinderType cylinderType, Quantity available) : IEvent
{
    public ECylinderType CylinderType { get; } = cylinderType;
    public Quantity      Available    { get; } = available;
}
