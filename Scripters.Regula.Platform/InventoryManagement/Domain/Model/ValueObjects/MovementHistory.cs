using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

public class MovementHistory
{
    public MovementHistory()
    {
        Movements = new List<Movement>();
    }

    public ICollection<Movement> Movements { get; private set; }

    internal void Add(Movement movement)
        => Movements.Add(movement);
}
