using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;

public interface ICustomerDebtCommandService
{
    Task<Result<CommercialDebt>> Handle(CreateCustomerDebtCommand command, CancellationToken cancellationToken = default);
}