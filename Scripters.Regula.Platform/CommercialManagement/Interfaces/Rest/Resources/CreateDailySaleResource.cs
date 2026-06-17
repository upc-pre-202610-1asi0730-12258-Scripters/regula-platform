namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record CreateDailySaleResource(
    string CylinderTypeId,
    string CylinderType,
    int Quantity,
    decimal UnitPrice,
    string PaymentType,
    int? CustomerId,
    string? CustomerName,
    string? DistributorName
);