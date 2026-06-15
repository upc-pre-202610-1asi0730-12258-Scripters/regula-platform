namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Represents the origin or destination of an inventory movement.</summary>
/// <param name="Value">The origin or destination name.</param>
public record OriginDestination(string Value)
{
    public OriginDestination() : this(string.Empty)
    {
    }
}
