using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;

public interface IDailySaleCommandService
{
    Task<Result<CommercialDailySale>> Handle(CreateDailySaleCommand command, CancellationToken cancellationToken = default);
}