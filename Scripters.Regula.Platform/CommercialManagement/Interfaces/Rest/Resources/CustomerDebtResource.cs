namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record CustomerDebtResource(
    int Id,
    int CustomerId,
    decimal Amount,
    decimal RemainingAmount,
    string Description,
    string Status,
    DateTime? DueDate,
    DateTimeOffset? CreatedAt
);