using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Events;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;

public class StockDecreasedEvent(ECylinderType cylinderType, Quantity quantity) : IEvent
{
    public ECylinderType CylinderType { get; } = cylinderType;
    public Quantity      Quantity     { get; } = quantity;
}
