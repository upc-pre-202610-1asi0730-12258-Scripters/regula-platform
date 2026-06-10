namespace Scripters.Regula.Platform.Publishing.Domain.Model.Queries;

public record GetDashboardKpisQuery(int CompanyId);

public record GetInventoryReportQuery(
    int CompanyId,
    DateTime From,
    DateTime To,
    int? WarehouseId = null);

public record GetDeliveryReportQuery(
    int CompanyId,
    DateTime From,
    DateTime To);

public record GetCollectionsReportQuery(
    int CompanyId,
    DateTime From,
    DateTime To);