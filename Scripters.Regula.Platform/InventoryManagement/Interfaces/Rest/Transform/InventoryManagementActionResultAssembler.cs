using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Shared.Application.Model;
using Scripters.Regula.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Scripters.Regula.Platform.InventoryManagement.Resources;

namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Transform;

public static class InventoryManagementActionResultAssembler
{
    private static int ToStatusCode(InventoryManagementError error)
    {
        return error switch
        {
            InventoryManagementError.InventoryNotFound    => StatusCodes.Status404NotFound,
            InventoryManagementError.InvalidInventoryType => StatusCodes.Status422UnprocessableEntity,
            InventoryManagementError.InsufficientStock    => StatusCodes.Status422UnprocessableEntity,
            InventoryManagementError.InvalidProviderName  => StatusCodes.Status422UnprocessableEntity,
            InventoryManagementError.InvalidOutboundType  => StatusCodes.Status422UnprocessableEntity,
            InventoryManagementError.OperationCancelled   => StatusCodes.Status409Conflict,
            InventoryManagementError.DatabaseError        => StatusCodes.Status500InternalServerError,
            InventoryManagementError.InternalServerError  => StatusCodes.Status500InternalServerError,
            _                                             => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromGetInventoryByIdResult(
        ControllerBase controller,
        Inventory? inventory,
        IStringLocalizer<InventoryManagementMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Inventory, IActionResult> successAction)
    {
        if (inventory is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCode(InventoryManagementError.InventoryNotFound),
                InventoryManagementError.InventoryNotFound,
                errorLocalizer[nameof(InventoryManagementError.InventoryNotFound)]);
        return successAction(inventory);
    }

    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<InventoryManagementMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((InventoryManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
