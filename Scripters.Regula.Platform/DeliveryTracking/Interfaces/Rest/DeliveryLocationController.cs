using Microsoft.AspNetCore.Mvc;
using Scripters.Regula.Platform.DeliveryTracking.Application.QueryServices;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Errors;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Queries;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Scripters.Regula.Platform.DeliveryTracking.Interfaces.Rest;

[ApiController]
[Route("api/v1/deliveries")]
[Produces("application/json")]
public class DeliveryLocationController(IDeliveryLocationQueryService deliveryLocationQueryService) : ControllerBase
{
    [HttpGet("{id}/location")]
    [SwaggerOperation(
        Summary = "Get delivery GPS location",
        Description = "Returns the current GPS coordinates of the assigned driver for a delivery. " +
                      "If the driver has sent a signal within the last 5 minutes, returns active location. " +
                      "If more than 6 minutes have elapsed without a signal, returns NO_SIGNAL status with the last known position.",
        OperationId = "GetDeliveryLocation")]
    [SwaggerResponse(StatusCodes.Status200OK, "Driver location returned (active or no-signal)", typeof(object))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Delivery location not found")]
    public async Task<IActionResult> GetDeliveryLocation(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetDeliveryLocationQuery(id);
        var result = await deliveryLocationQueryService.Handle(query, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                DeliveryTrackingErrors.LocationNotFound => NotFound(new { error = result.Message }),
                DeliveryTrackingErrors.DeliveryNotFound => NotFound(new { error = result.Message }),
                _ => BadRequest(new { error = result.Message })
            };
        }

        var location = result.Value!;

        if (location.HasActiveSignal())
            return Ok(DeliveryLocationResourceFromEntityAssembler.ToActiveLocationResource(location));

        return Ok(DeliveryLocationResourceFromEntityAssembler.ToNoSignalLocationResource(location));
    }
}
