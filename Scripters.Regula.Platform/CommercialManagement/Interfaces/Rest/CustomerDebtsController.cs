using Microsoft.AspNetCore.Mvc;
using Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Errors;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/customer-debts")]
[Produces("application/json")]
public class CustomerDebtsController(ICustomerDebtCommandService customerDebtCommandService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create customer debt",
        Description = "Creates a customer debt without creating a sale, payment, stock movement or debt movement.",
        OperationId = "CreateCustomerDebt")]
    [SwaggerResponse(StatusCodes.Status201Created, "Customer debt created", typeof(CustomerDebtResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found")]
    public async Task<IActionResult> CreateCustomerDebt(
        [FromBody] CreateCustomerDebtResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateCustomerDebtCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await customerDebtCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                CommercialManagementErrors.CustomerNotFound => NotFound(new { error = result.Message }),
                CommercialManagementErrors.InvalidDebtAmount => BadRequest(new { error = result.Message }),
                CommercialManagementErrors.InvalidDebtDescription => BadRequest(new { error = result.Message }),
                _ => BadRequest(new { error = result.Message })
            };
        }

        var customerDebtResource = CustomerDebtResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return StatusCode(StatusCodes.Status201Created, customerDebtResource);
    }
}