using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

public partial class GasCylinderStock : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
