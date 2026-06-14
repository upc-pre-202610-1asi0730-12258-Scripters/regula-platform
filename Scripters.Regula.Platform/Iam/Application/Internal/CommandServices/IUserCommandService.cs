using Scripters.Regula.Platform.Iam.Application.Commands;
using Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;

namespace Scripters.Regula.Platform.Iam.Application.Internal.CommandServices;

public interface IUserCommandService
{
 
    Task<(User user, string token)> Handle(SignInCommand command);
    Task Handle(SignUpCommand command);
}
