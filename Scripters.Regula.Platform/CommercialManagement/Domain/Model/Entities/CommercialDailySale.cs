using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Shared.Domain.Model.Entities;

namespace Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;

public class CommercialDailySale : IAuditableEntity
{
    public int Id { get; private set; }

    public string TransactionCode { get; private set; } = string.Empty;

    public string CylinderTypeId { get; private set; } = string.Empty;

    public string CylinderType { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalAmount { get; private set; }

    public ECommercialPaymentType PaymentType { get; private set; }

    public int? CustomerId { get; private set; }

    public CommercialCustomer? Customer { get; private set; }

    public string? CustomerName { get; private set; }

    public string DistributorName { get; private set; } = string.Empty;

    public ECommercialSaleStatus Status { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    protected CommercialDailySale()
    {
    }

    public CommercialDailySale(
        string cylinderTypeId,
        string cylinderType,
        int quantity,
        decimal unitPrice,
        ECommercialPaymentType paymentType,
        int? customerId,
        string? customerName,
        string? distributorName)
    {
        TransactionCode = GenerateTransactionCode();
        CylinderTypeId = cylinderTypeId;
        CylinderType = cylinderType;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalAmount = quantity * unitPrice;
        PaymentType = paymentType;
        CustomerId = customerId;
        CustomerName = customerName;
        DistributorName = string.IsNullOrWhiteSpace(distributorName) ? "Admin" : distributorName.Trim();
        Status = ECommercialSaleStatus.Active;
    }

    public bool IsDebtSale()
    {
        return PaymentType == ECommercialPaymentType.Debt;
    }

    public string BuildDebtDescription()
    {
        return $"{Quantity} balón(es) {CylinderType}";
    }

    private static string GenerateTransactionCode()
    {
        var suffix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 1000000;
        return $"VT-{suffix:D6}";
    }
}