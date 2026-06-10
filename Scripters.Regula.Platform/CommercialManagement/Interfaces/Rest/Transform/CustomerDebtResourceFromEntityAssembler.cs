using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class CustomerDebtResourceFromEntityAssembler
{
    public static CustomerDebtResource ToResourceFromEntity(CommercialDebt entity)
    {
        return new CustomerDebtResource(
            entity.Id,
            entity.CustomerId,
            entity.Amount,
            entity.RemainingAmount,
            entity.Description,
            entity.Status.ToString().ToUpperInvariant(),
            entity.DueDate,
            entity.CreatedAt);
    }
}