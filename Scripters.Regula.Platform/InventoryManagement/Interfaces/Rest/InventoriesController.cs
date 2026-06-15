using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scripters.Regula.Platform.InventoryManagement.Application.QueryServices;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Queries;
using Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Transform;
using Scripters.Regula.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Scripters.Regula.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/inventories")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Inventory Management Endpoints.")]
public class InventoriesController(
    IInventoryQueryService inventoryQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{inventoryId:long}")]
    [SwaggerOperation(Summary = "Get inventory by id", OperationId = "GetInventoryById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Inventory", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Inventory not found")]
    public async Task<IActionResult> GetInventoryById(
        [FromRoute] long inventoryId,
        CancellationToken cancellationToken)
    {
        var inventory = await inventoryQueryService.Handle(
            new GetInventoryByIdQuery(inventoryId), cancellationToken);

        return InventoryManagementActionResultAssembler.ToActionResultFromGetInventoryByIdResult(
            this, inventory, _errorLocalizer, _problemDetailsFactory,
            found => Ok(InventoryResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }
}
