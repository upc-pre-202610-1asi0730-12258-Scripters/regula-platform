using Scripters.Regula.Platform.Iam.Application.Commands;
using Scripters.Regula.Platform.Iam.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.Iam.Interfaces.Rest.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}
