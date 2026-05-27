using Scripters.Regula.Plataform.Shared.Domain.Repositories;
using Scripters.Regula.Plataform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Scripters.Regula.Plataform.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}