namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Free-text observation attached to an inventory movement.</summary>
/// <param name="Value">The observation text.</param>
public record Observation(string Value)
{
    public Observation() : this(string.Empty)
    {
    }
}
