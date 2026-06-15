namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Captures the serialized state of an entity after a mutation.</summary>
/// <param name="Value">The serialized new state (e.g. JSON string).</param>
public record NewValue(string Value)
{
    public NewValue() : this(string.Empty)
    {
    }
}
