namespace Scripters.Regula.Platform.Report.Domain.Model.ValueObjects;

public record InventorySummary(
    int TotalEntries,
    int TotalExits,
    int NetVariation,
    IEnumerable<CylinderTypeBreakdown> ByType);

public record CylinderTypeBreakdown(
    string CylinderType,
    int Entries,
    int Exits
    );

public record DeliverySummary(
    int TotalOrders,
    int Delivered,
    int PartiallyDelivered,
    int Cancelled,
    double FulfillmentRate,
    double AverageDeliveryTimeMinutes,
    int TotalCylindersDelivered
    );

public record CollectionsSummary(
    decimal TotalCollected,
    decimal TotalPendingDebt,
    IEnumerable<TopDebtor> TopDebtors
    );

public record TopDebtor(
    int ClientId,
    string ClientName,
    decimal PendingAmount
    );

public record DashboardKpis(
    int ActiveAlerts,
    int TotalStock,
    int ActiveSensors,
    int PendingDeliveries,
    DateTime GeneratedAt
    );
