using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Scripters.Regula.Platform.Iam.Application.Internal.CommandServices;
using Scripters.Regula.Platform.Iam.Interfaces.Rest.Resources;
using Scripters.Regula.Platform.Iam.Interfaces.Rest.Transform;

namespace Scripters.Regula.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("/api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { message = "User created successfully" });
    }
}
