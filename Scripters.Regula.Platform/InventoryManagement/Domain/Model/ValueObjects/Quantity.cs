namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents a quantity value object.
///     Value must be greater than zero when validated.
///     A value of zero is permitted in stock initialisation contexts.
/// </summary>
/// <param name="Value">The quantity amount.</param>
public record Quantity(int Value)
{
    public Quantity() : this(0)
    {
    }

    /// <summary>Throws when value is not greater than zero.</summary>
    public void Validate()
    {
        if (Value <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(Value));
    }
}
