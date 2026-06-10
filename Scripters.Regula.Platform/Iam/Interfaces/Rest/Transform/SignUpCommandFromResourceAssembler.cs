using Scripters.Regula.Platform.Iam.Application.Commands;
using Scripters.Regula.Platform.Iam.Interfaces.Rest.Resources;

namespace Scripters.Regula.Platform.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}
