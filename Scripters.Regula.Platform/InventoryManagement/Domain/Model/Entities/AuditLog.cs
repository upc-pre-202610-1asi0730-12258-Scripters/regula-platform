using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

public class AuditLog
{
    public AuditLog()
    {
        EntityName    = string.Empty;
        PreviousValue = new PreviousValue(string.Empty);
        NewValue      = new NewValue(string.Empty);
        ProfileId     = new ProfileId(0);
    }

    public AuditLog(
        string              entityName,
        long                entityId,
        EAuditOperationType operationType,
        PreviousValue       previousValue,
        NewValue            newValue,
        ProfileId           profileId)
    {
        EntityName    = entityName;
        EntityId      = entityId;
        OperationType = operationType;
        PreviousValue = previousValue;
        NewValue      = newValue;
        ProfileId     = profileId;
    }

    public int Id { get; private set; }

    public string              EntityName    { get; private set; }
    public long                EntityId      { get; private set; }
    public EAuditOperationType OperationType { get; private set; }
    public PreviousValue       PreviousValue { get; private set; }
    public NewValue            NewValue      { get; private set; }
    public ProfileId           ProfileId     { get; private set; }
}
