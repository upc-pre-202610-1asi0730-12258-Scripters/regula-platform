using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Scripters.Regula.Platform.Shared.Resources.Errors;

namespace Scripters.Regula.Platform.InventoryManagement.Interfaces.Rest.Transform;

public static class InventoryManagementActionResultAssembler
{
    private static int ToStatusCode(InventoryManagementError error)
    {
        return error switch
        {
            InventoryManagementError.InventoryNotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromGetInventoryByIdResult(
        ControllerBase controller,
        Inventory? inventory,
        IStringLocalizer<ErrorMessages> errorLocalizer,
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
}
