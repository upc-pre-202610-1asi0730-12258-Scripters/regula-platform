using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.InventoryManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.InventoryManagement.Application.CommandServices;

public interface IInventoryCommandService
{
    Task<Result<CompanyMovement>> Handle(CreateCompanyMovementCommand command, CancellationToken cancellationToken);

    Task<Result<DistributorMovement>> Handle(CreateDistributorMovementCommand command, CancellationToken cancellationToken);
}
