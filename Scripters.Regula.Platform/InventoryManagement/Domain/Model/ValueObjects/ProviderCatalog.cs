using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

public class ProviderCatalog
{
    public ProviderCatalog()
    {
        Providers = new List<Provider>();
    }

    public ICollection<Provider> Providers { get; private set; }

    internal void Add(Provider provider)
        => Providers.Add(provider);
}
