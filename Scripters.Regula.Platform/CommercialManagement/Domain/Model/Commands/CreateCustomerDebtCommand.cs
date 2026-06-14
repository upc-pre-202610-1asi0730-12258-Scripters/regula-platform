namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;

public record CreateCustomerDebtCommand(
    int CustomerId,
    decimal Amount,
    string Description,
    DateTime? DueDate
);