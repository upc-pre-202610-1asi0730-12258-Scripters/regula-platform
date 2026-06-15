namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     External identifier referencing a profile in the IAM Bounded Context.
///     Sole coupling point with IAM BC.
///     Only stores the identifier – does not navigate into IAM BC.
///     Referenced by: Movement (and subclasses), AuditLog.
/// </summary>
/// <param name="Value">The profile identifier.</param>
public record ProfileId(long Value)
{
    public ProfileId() : this(0)
    {
    }
}
