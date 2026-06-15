using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class CustomerDebtPaymentResourceFromEntityAssembler
{
    public static CustomerDebtPaymentResource ToResourceFromEntity(CommercialDebtPayment entity)
    {
        return new CustomerDebtPaymentResource(
            entity.Id,
            entity.CustomerDebtId,
            entity.CustomerId,
            entity.Amount,
            entity.PreviousRemainingAmount,
            entity.NewRemainingAmount,
            entity.Note,
            entity.CreatedAt);
    }
}