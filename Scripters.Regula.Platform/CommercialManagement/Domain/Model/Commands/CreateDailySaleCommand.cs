namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;

public record CreateDailySaleCommand(
    string CylinderTypeId,
    string CylinderType,
    int Quantity,
    decimal UnitPrice,
    string PaymentType,
    int? CustomerId,
    string? CustomerName,
    string? DistributorName
);