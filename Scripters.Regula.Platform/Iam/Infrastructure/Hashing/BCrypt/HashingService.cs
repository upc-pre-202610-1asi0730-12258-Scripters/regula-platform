using BCryptNet = BCrypt.Net.BCrypt;
using Scripters.Regula.Platform.Iam.Application.Internal.OutboundServices;

namespace Scripters.Regula.Platform.Iam.Infrastructure.Hashing.BCrypt;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}
