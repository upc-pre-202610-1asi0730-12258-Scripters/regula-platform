using Microsoft.AspNetCore.Mvc;
using Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Application.QueryServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Errors;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Queries;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Scripters.Regula.Platform.CommercialManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/daily-sales")]
[Produces("application/json")]
public class DailySalesController(
    IDailySaleCommandService dailySaleCommandService,
    IDailySaleQueryService dailySaleQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all daily sales",
        Description = "Gets all registered daily sales ordered by creation date descending.",
        OperationId = "GetAllDailySales")]
    [SwaggerResponse(StatusCodes.Status200OK, "Daily sales found", typeof(IEnumerable<DailySaleResource>))]
    public async Task<IActionResult> GetAllDailySales(CancellationToken cancellationToken)
    {
        var query = new GetAllDailySalesQuery();

        var dailySales = await dailySaleQueryService.Handle(query, cancellationToken);

        var dailySaleResources = dailySales
            .Select(DailySaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(dailySaleResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create daily sale",
        Description = "Creates a daily sale. If payment type is DEBT, it also creates the related customer debt. It does not update inventory stock.",
        OperationId = "CreateDailySale")]
    [SwaggerResponse(StatusCodes.Status201Created, "Daily sale created", typeof(DailySaleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found")]
    public async Task<IActionResult> CreateDailySale(
        [FromBody] CreateDailySaleResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateDailySaleCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await dailySaleCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                CommercialManagementErrors.CustomerNotFound => NotFound(new { error = result.Message }),
                CommercialManagementErrors.InvalidCylinderType => BadRequest(new { error = result.Message }),
                CommercialManagementErrors.InvalidSaleQuantity => BadRequest(new { error = result.Message }),
                CommercialManagementErrors.InvalidUnitPrice => BadRequest(new { error = result.Message }),
                CommercialManagementErrors.InvalidPaymentType => BadRequest(new { error = result.Message }),
                CommercialManagementErrors.CustomerRequiredForDebtSale => BadRequest(new { error = result.Message }),
                _ => BadRequest(new { error = result.Message })
            };
        }

        var dailySaleResource = DailySaleResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return StatusCode(StatusCodes.Status201Created, dailySaleResource);
    }
}