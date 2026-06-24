using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Events;
using Scripters.Regula.Platform.Shared.Application.Internal.EventHandlers;

namespace Scripters.Regula.Platform.InventoryManagement.Application.Internal.EventHandlers;

public class MovementRegisteredEventHandler : IEventHandler<MovementRegisteredEvent>
{
    public Task Handle(MovementRegisteredEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Movement registered: Id={0}, Type={1}, Cylinder={2}, Qty={3}",
            domainEvent.MovementId, domainEvent.MovementType, domainEvent.CylinderType, domainEvent.Quantity.Value);
        return Task.CompletedTask;
    }
}
