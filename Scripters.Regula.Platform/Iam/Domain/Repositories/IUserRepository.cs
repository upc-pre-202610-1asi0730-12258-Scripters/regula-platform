using Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;

namespace Scripters.Regula.Platform.Iam.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username);
    Task AddAsync(User user);
}
