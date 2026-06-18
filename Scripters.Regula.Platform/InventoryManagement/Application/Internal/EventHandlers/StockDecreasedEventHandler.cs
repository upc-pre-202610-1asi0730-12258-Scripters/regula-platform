using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;
using Scripters.Regula.Platform.Shared.Application.Internal.EventHandlers;

namespace Scripters.Regula.Platform.InventoryManagement.Application.Internal.EventHandlers;

public class StockDecreasedEventHandler : IEventHandler<StockDecreasedEvent>
{
    public Task Handle(StockDecreasedEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Stock decreased: Cylinder={0}, Qty={1}",
            domainEvent.CylinderType, domainEvent.Quantity.Value);
        return Task.CompletedTask;
    }
}
