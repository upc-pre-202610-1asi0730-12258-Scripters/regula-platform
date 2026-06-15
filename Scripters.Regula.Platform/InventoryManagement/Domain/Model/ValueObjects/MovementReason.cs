namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Represents the business reason behind an inventory movement.</summary>
/// <param name="Value">The reason description.</param>
public record MovementReason(string Value)
{
    public MovementReason() : this(string.Empty)
    {
    }
}
