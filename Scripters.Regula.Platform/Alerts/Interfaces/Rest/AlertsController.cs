using Microsoft.AspNetCore.Mvc;
using Scripters.Regula.Platform.Alerts.Application.QueryServices;
using Scripters.Regula.Platform.Alerts.Domain.Model.Queries;
using Scripters.Regula.Platform.Alerts.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.Alerts.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.Alerts.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Scripters.Regula.Platform.Alerts.Interfaces.Rest;

[ApiController]
[Route("api/v1/alerts")]
[Produces("application/json")]
public class AlertsController(IAlertQueryService alertQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get alerts by status",
        Description = "Returns alerts filtered by status ordered by criticality descending (HIGH > MEDIUM > LOW).",
        OperationId = "GetAlerts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alerts returned", typeof(IEnumerable<AlertResource>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid status value")]
    public async Task<IActionResult> GetAlerts(
        [FromQuery] string status,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<EAlertStatus>(status, ignoreCase: true, out var alertStatus))
            return BadRequest(new { error = $"Invalid status value '{status}'. Allowed values: PENDING, RESOLVED." });

        var query = new GetAlertsByStatusQuery(alertStatus);
        var result = await alertQueryService.Handle(query, cancellationToken);

        var resources = result.Value!.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
