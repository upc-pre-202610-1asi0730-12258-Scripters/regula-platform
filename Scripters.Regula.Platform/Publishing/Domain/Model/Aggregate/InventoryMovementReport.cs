namespace Scripters.Regula.Platform.Publishing.Domain.Model.Aggregate;

public class InventoryMovementReport
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string MovementType { get; set; } = string.Empty;

    public string CylinderType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public DateTime MovementDate { get; set; }

    public string WarehouseName { get; set; } = string.Empty;
}