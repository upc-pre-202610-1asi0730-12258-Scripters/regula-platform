using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class DailySaleResourceFromEntityAssembler
{
    public static DailySaleResource ToResourceFromEntity(CommercialDailySale entity)
    {
        return new DailySaleResource(
            entity.Id,
            entity.TransactionCode,
            entity.CylinderTypeId,
            entity.CylinderType,
            entity.Quantity,
            entity.UnitPrice,
            entity.TotalAmount,
            entity.PaymentType.ToString().ToUpperInvariant(),
            entity.CustomerId,
            entity.CustomerName,
            entity.DistributorName,
            entity.Status.ToString().ToUpperInvariant(),
            entity.CreatedAt);
    }
}