namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>Represents the name of a provider. Must not be blank.</summary>
/// <param name="Value">The provider name string.</param>
public record ProviderName(string Value)
{
    public ProviderName() : this(string.Empty)
    {
    }

    /// <summary>Throws when value is null or whitespace.</summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Provider name must not be blank.", nameof(Value));
    }
}
