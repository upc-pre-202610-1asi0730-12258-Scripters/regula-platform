using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Queries;

namespace Scripters.Regula.Platform.CommercialManagement.Application.QueryServices;

public interface IDailySaleQueryService
{
    Task<IEnumerable<CommercialDailySale>> Handle(
        GetAllDailySalesQuery query,
        CancellationToken cancellationToken = default);
}