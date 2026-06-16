using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Commands;

public record UpdateDeliveryStatusCommand(int DeliveryId, EDeliveryStatus Status, string? DeliveredAt);
