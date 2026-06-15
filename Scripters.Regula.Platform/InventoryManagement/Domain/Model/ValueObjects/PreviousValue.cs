namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Captures the serialized state of an entity before a mutation.</summary>
/// <param name="Value">The serialized previous state (e.g. JSON string).</param>
public record PreviousValue(string Value)
{
    public PreviousValue() : this(string.Empty)
    {
    }
}
