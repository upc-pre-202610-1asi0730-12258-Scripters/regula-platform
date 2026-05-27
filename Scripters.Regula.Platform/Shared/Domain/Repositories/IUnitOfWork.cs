namespace Scripters.Regula.Plataform.Shared.Domain.Repositories;

public interface IUnitOfWork 
{
    Task CompleteAsync();
    
}