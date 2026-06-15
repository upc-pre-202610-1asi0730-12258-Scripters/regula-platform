using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

public partial class Provider
{
    public Provider()
    {
        ProviderName = new ProviderName("N/A");
    }

    public Provider(ProviderName providerName)
    {
        providerName.Validate();
        ProviderName = providerName;
    }

    public int Id { get; private set; }

    public ProviderName ProviderName { get; private set; }
}
