namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record CustomerDebtPaymentResource(
    int Id,
    int CustomerDebtId,
    int CustomerId,
    decimal Amount,
    decimal PreviousRemainingAmount,
    decimal NewRemainingAmount,
    string? Note,
    DateTimeOffset? CreatedAt
);