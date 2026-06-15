using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;

public class CommercialDebt : IAuditableEntity
{
    public int Id { get; private set; }

    public int CustomerId { get; private set; }

    public CommercialCustomer Customer { get; private set; } = null!;

    public decimal Amount { get; private set; }

    public decimal RemainingAmount { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public ECommercialDebtStatus Status { get; private set; }

    public DateTime? DueDate { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected CommercialDebt()
    {
    }

    public CommercialDebt(int customerId, decimal amount, string description, DateTime? dueDate)
    {
        CustomerId = customerId;
        Amount = amount;
        RemainingAmount = amount;
        Description = description;
        DueDate = dueDate;
        Status = ECommercialDebtStatus.Pending;
    }

    public bool IsPending()
    {
        return Status == ECommercialDebtStatus.Pending;
    }

    public CommercialDebtPayment RegisterPayment(decimal amount, string? note)
    {
        var previousRemainingAmount = RemainingAmount;

        RemainingAmount -= amount;

        if (RemainingAmount <= 0)
        {
            RemainingAmount = 0;
            Status = ECommercialDebtStatus.Paid;
        }

        return new CommercialDebtPayment(
            Id,
            CustomerId,
            amount,
            previousRemainingAmount,
            RemainingAmount,
            note);
    }
}