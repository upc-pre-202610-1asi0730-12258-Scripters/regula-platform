namespace Scripters.Regula.Platform.Shared.Domain.Repositories;

public interface IUnitOfWork 
{
    Task CompleteAsync();
    
}