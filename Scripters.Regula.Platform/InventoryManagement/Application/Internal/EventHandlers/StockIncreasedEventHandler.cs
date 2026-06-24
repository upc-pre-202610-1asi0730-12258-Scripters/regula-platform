using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;
using Scripters.Regula.Platform.Shared.Application.Internal.EventHandlers;

namespace Scripters.Regula.Platform.InventoryManagement.Application.Internal.EventHandlers;

public class StockIncreasedEventHandler : IEventHandler<StockIncreasedEvent>
{
    public Task Handle(StockIncreasedEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Stock increased: Cylinder={0}, Qty={1}",
            domainEvent.CylinderType, domainEvent.Quantity.Value);
        return Task.CompletedTask;
    }
}
