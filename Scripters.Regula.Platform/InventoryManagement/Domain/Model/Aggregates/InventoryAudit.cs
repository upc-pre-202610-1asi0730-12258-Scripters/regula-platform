using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;

public partial class Inventory : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
