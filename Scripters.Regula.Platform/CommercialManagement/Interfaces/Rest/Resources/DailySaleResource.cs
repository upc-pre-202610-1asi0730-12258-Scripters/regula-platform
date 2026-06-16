namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

public record DailySaleResource(
    int Id,
    string TransactionCode,
    string CylinderTypeId,
    string CylinderType,
    int Quantity,
    decimal UnitPrice,
    decimal TotalAmount,
    string PaymentType,
    int? CustomerId,
    string? CustomerName,
    string DistributorName,
    string Status,
    DateTimeOffset? CreatedAt
);