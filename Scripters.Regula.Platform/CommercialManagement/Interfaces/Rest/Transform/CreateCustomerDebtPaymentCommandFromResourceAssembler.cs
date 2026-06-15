using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class CreateCustomerDebtPaymentCommandFromResourceAssembler
{
    public static CreateCustomerDebtPaymentCommand ToCommandFromResource(
        int customerDebtId,
        CreateCustomerDebtPaymentResource resource)
    {
        return new CreateCustomerDebtPaymentCommand(
            customerDebtId,
            resource.Amount,
            resource.Note);
    }
}