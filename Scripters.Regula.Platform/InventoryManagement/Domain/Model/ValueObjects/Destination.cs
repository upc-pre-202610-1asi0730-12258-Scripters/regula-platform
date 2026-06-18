namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Represents the destination of an inventory movement.</summary>
/// <param name="Value">The destination name.</param>
public record Destination(string Value)
{
    public Destination() : this(string.Empty)
    {
    }
}
