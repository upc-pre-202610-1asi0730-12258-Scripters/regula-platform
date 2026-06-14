using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class CreateCustomerDebtCommandFromResourceAssembler
{
    public static CreateCustomerDebtCommand ToCommandFromResource(CreateCustomerDebtResource resource)
    {
        return new CreateCustomerDebtCommand(
            resource.CustomerId,
            resource.Amount,
            resource.Description,
            resource.DueDate);
    }
}