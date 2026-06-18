using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

public class MovementHistory
{
    public MovementHistory()
    {
        Movements = new List<Movement>();
    }

    public ICollection<Movement> Movements { get; private set; }

    public IReadOnlyCollection<Movement> FilterByType(EMovementType type)
        => Movements.Where(m => m.MovementType == type).ToList();

    public IReadOnlyCollection<TMovement> OfType<TMovement>() where TMovement : Movement
        => Movements.OfType<TMovement>().ToList();

    internal void Add(Movement movement)
        => Movements.Add(movement);
}
