using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

public class DistributorMovement : Movement
{
    public DistributorMovement()
    {
        ProviderName = new ProviderName("N/A");
    }

    public DistributorMovement(
        EMovementType movementType,
        ECylinderType cylinderType,
        Quantity      quantity,
        ProviderName  providerName,
        ProfileId     profileId)
        : base(movementType, cylinderType, quantity, profileId)
    {
        ProviderName = providerName;
    }

    public ProviderName ProviderName { get; private set; }
}
