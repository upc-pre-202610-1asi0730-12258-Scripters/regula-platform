using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;
using Scripters.Regula.Platform.Shared.Application.Internal.EventHandlers;

namespace Scripters.Regula.Platform.InventoryManagement.Application.Internal.EventHandlers;

public class LowStockDetectedEventHandler : IEventHandler<LowStockDetectedEvent>
{
    public Task Handle(LowStockDetectedEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Low stock detected: Cylinder={0}, Available={1}",
            domainEvent.CylinderType, domainEvent.Available.Value);
        return Task.CompletedTask;
    }
}
