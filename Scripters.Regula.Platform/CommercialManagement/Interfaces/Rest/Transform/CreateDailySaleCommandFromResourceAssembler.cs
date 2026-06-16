using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;

public static class CreateDailySaleCommandFromResourceAssembler
{
    public static CreateDailySaleCommand ToCommandFromResource(CreateDailySaleResource resource)
    {
        return new CreateDailySaleCommand(
            resource.CylinderTypeId,
            resource.CylinderType,
            resource.Quantity,
            resource.UnitPrice,
            resource.PaymentType,
            resource.CustomerId,
            resource.CustomerName,
            resource.DistributorName);
    }
}