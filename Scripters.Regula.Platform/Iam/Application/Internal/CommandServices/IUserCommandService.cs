using Scripters.Regula.Platform.Iam.Application.Commands;

namespace Scripters.Regula.Platform.Iam.Application.Internal.CommandServices;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
}
