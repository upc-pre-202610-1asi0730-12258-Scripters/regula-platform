namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;

public record CreateCustomerDebtPaymentCommand(
    int CustomerDebtId,
    decimal Amount,
    string? Note
);