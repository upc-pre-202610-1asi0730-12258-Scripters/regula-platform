using Scripters.Regula.Platform.Shared.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    // inheritedDoc
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    // inheritedDoc
    public Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    // inheritedDoc
    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    // inheritedDoc
    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    // inheritedDoc
    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().ToListAsync(cancellationToken);
    }
}