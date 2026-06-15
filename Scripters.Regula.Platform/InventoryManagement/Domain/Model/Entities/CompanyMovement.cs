using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

public class CompanyMovement : Movement
{
    public CompanyMovement()
    {
        OriginDestination = new OriginDestination(string.Empty);
        MovementReason    = new MovementReason(string.Empty);
        Observation       = new Observation(string.Empty);
    }

    public CompanyMovement(
        EMovementType     movementType,
        ECylinderType     cylinderType,
        Quantity          quantity,
        OriginDestination originDestination,
        MovementReason    movementReason,
        Observation       observation,
        ProfileId         profileId)
        : base(movementType, cylinderType, quantity, profileId)
    {
        OriginDestination = originDestination;
        MovementReason    = movementReason;
        Observation       = observation;
    }

    public OriginDestination OriginDestination { get; private set; }
    public MovementReason    MovementReason    { get; private set; }
    public Observation       Observation       { get; private set; }
}
