using Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;

namespace Scripters.Regula.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
}
