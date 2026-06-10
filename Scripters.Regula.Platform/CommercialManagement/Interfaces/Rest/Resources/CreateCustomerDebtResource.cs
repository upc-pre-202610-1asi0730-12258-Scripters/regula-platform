namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record CreateCustomerDebtResource(
    int CustomerId,
    decimal Amount,
    string Description,
    DateTime? DueDate
);