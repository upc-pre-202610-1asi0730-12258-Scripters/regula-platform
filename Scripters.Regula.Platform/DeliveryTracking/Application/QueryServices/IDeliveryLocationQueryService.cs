using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Queries;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.DeliveryTracking.Application.QueryServices;

public interface IDeliveryLocationQueryService
{
    Task<Result<DriverLocation>> Handle(GetDeliveryLocationQuery query, CancellationToken cancellationToken = default);
}
