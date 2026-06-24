using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Value Object Collection that encapsulates all <see cref="GasCylinderStock" /> items
///     owned by an Inventory aggregate.
///
///     The Inventory aggregate must never hold a raw List&lt;GasCylinderStock&gt; directly;
///     it must always use this collection to honour DDD encapsulation.
/// </summary>
public class StockSummary
{
    public StockSummary()
    {
        StockItems = new List<GasCylinderStock>();
    }

    /// <summary>All stock items tracked by this inventory.</summary>
    public ICollection<GasCylinderStock> StockItems { get; private set; }

    /// <summary>Returns the stock entry for a given cylinder type, or null if not yet tracked.</summary>
    public GasCylinderStock? GetByType(ECylinderType cylinderType)
        => StockItems.FirstOrDefault(s => s.CylinderType == cylinderType);

    /// <summary>Returns the total available units across all cylinder types.</summary>
    public int TotalAvailable()
        => StockItems.Sum(s => s.Available.Value);

    internal void Add(GasCylinderStock stock)
        => StockItems.Add(stock);
}
