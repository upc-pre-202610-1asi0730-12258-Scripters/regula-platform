using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;

public class DeliveryVehicle : IAuditableEntity
{
    public int Id { get; private set; }

    public string Plate { get; private set; } = string.Empty;

    public string Type { get; private set; } = string.Empty;

    public string Brand { get; private set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected DeliveryVehicle()
    {
    }

    public DeliveryVehicle(string plate, string type, string brand)
    {
        Plate = plate;
        Type = type;
        Brand = brand;
    }
}
