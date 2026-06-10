using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.Shared.Domain.Repositories;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

public abstract class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }
}
