using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;

public class CommercialDebtPayment : IAuditableEntity
{
    public int Id { get; private set; }

    public int CustomerDebtId { get; private set; }

    public CommercialDebt CustomerDebt { get; private set; } = null!;

    public int CustomerId { get; private set; }

    public decimal Amount { get; private set; }

    public decimal PreviousRemainingAmount { get; private set; }

    public decimal NewRemainingAmount { get; private set; }

    public string? Note { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected CommercialDebtPayment()
    {
    }

    public CommercialDebtPayment(
        int customerDebtId,
        int customerId,
        decimal amount,
        decimal previousRemainingAmount,
        decimal newRemainingAmount,
        string? note)
    {
        CustomerDebtId = customerDebtId;
        CustomerId = customerId;
        Amount = amount;
        PreviousRemainingAmount = previousRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        Note = note;
    }
}