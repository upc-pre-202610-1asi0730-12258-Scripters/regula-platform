using Scripters.Regula.Platform.InventoryManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;

/// <summary>
///     Represents the stock level of a specific gas cylinder type.
///     Belongs to the Inventory aggregate through <see cref="StockSummary" />.
///
///     Split across partial files:
///     <list type="bullet">
///         <item><c>GasCylinderStock.cs</c>      – identity, state and behaviour</item>
///         <item><c>GasCylinderStockAudit.cs</c> – IAuditableEntity implementation</item>
///     </list>
/// </summary>
public partial class GasCylinderStock
{
    public GasCylinderStock()
    {
        Available    = new Quantity(0);
        InTransit    = new Quantity(0);
        Observed     = new Quantity(0);
        OutOfService = new Quantity(0);
    }

    public GasCylinderStock(
        ECylinderType cylinderType,
        Quantity      available,
        Quantity      inTransit,
        Quantity      observed,
        Quantity      outOfService)
    {
        CylinderType = cylinderType;
        Available    = available;
        InTransit    = inTransit;
        Observed     = observed;
        OutOfService = outOfService;
    }

    // ── Identity ─────────────────────────────────────────────────────────────
    public int Id { get; private set; }

    // ── State ─────────────────────────────────────────────────────────────────
    public ECylinderType CylinderType { get; private set; }
    public Quantity      Available    { get; private set; }
    public Quantity      InTransit    { get; private set; }
    public Quantity      Observed     { get; private set; }
    public Quantity      OutOfService { get; private set; }

    // ── Behaviour ────────────────────────────────────────────────────────────

    /// <summary>Returns the total count across all stock states.</summary>
    public int Total() => Available.Value + InTransit.Value + Observed.Value + OutOfService.Value;

    /// <summary>Returns true when available stock is below the given threshold.</summary>
    public bool IsLow(int threshold) => Available.Value < threshold;

    /// <summary>Increases the available stock by the given quantity.</summary>
    public void Increase(Quantity qty)
    {
        qty.Validate();
        Available = new Quantity(Available.Value + qty.Value);
    }

    /// <summary>Decreases the available stock by the given quantity.</summary>
    public void Decrease(Quantity qty)
    {
        qty.Validate();
        if (qty.Value > Available.Value)
            throw new InvalidOperationException(
                $"Insufficient available stock for {CylinderType}. " +
                $"Requested: {qty.Value}, Available: {Available.Value}.");
        Available = new Quantity(Available.Value - qty.Value);
    }
}
