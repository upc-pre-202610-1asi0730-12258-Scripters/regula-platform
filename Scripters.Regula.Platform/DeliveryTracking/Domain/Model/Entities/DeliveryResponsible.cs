using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;

public class DeliveryResponsible : IAuditableEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected DeliveryResponsible()
    {
    }

    public DeliveryResponsible(string name)
    {
        Name = name;
    }
}
