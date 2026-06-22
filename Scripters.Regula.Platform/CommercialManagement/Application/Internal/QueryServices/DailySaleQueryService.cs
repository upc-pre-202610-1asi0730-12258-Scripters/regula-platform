using Scripters.Regula.Platform.CommercialManagement.Application.QueryServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Queries;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;

namespace Scripters.Regula.Platform.CommercialManagement.Application.Internal.QueryServices;

public class DailySaleQueryService(ICommercialDailySaleRepository dailySaleRepository)
    : IDailySaleQueryService
{
    public async Task<IEnumerable<CommercialDailySale>> Handle(
        GetAllDailySalesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await dailySaleRepository.ListOrderedByCreatedAtDescendingAsync(cancellationToken);
    }
}