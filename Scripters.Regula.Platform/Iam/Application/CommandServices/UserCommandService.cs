using Scripters.Regula.Platform.Iam.Application.Commands;
using Scripters.Regula.Platform.Iam.Application.Internal.OutboundServices;
using Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Iam.Domain.Repositories;

namespace Scripters.Regula.Platform.Iam.Application.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService)
    : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new Exception("Invalid username or password.");
        }

        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}
