using Cortex.Mediator.Notifications;

namespace Scripters.Regula.Platform.Shared.Domain.Model.Events;

/// <summary>
///     Represents a domain event in the system.
/// </summary>
/// <remarks>
///     This interface is used to mark classes as domain events that can be published and handled by the event bus.
///     It extends from <see cref="INotification" /> to integrate with the mediator pattern for event handling.
/// </remarks>
public interface IEvent : INotification
{
    
}