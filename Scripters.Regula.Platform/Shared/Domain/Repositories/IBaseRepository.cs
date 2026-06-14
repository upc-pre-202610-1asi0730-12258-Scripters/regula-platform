namespace Scripters.Regula.Platform.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken = default);

    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);
}