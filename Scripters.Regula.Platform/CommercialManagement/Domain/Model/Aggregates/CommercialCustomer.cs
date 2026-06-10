using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Aggregates;

public class CommercialCustomer : IAuditableEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public decimal ActiveDebtAmount { get; private set; }

    public int DebtCount { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected CommercialCustomer()
    {
    }

    public CommercialCustomer(string name)
    {
        Name = name;
        ActiveDebtAmount = 0;
        DebtCount = 0;
    }

    public void IncreaseActiveDebt(decimal amount)
    {
        ActiveDebtAmount += amount;
        DebtCount++;
    }
}