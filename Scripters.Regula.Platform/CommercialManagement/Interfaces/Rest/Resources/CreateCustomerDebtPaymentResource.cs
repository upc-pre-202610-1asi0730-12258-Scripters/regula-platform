namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record CreateCustomerDebtPaymentResource(
    decimal Amount,
    string? Note
);