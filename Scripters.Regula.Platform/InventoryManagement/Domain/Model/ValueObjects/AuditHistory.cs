using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

public class AuditHistory
{
    public AuditHistory()
    {
        Logs = new List<AuditLog>();
    }

    public ICollection<AuditLog> Logs { get; private set; }

    internal void Add(AuditLog log)
        => Logs.Add(log);
}
