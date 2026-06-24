using Scripters.Regula.Platform.DeliveryTracking.Application.QueryServices;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Errors;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Entities;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Queries;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Application.Model;

namespace Scripters.Regula.Platform.DeliveryTracking.Application.Internal.QueryServices;

public class DeliveryLocationQueryService(
    IDriverLocationRepository driverLocationRepository) : IDeliveryLocationQueryService
{
    public async Task<Result<DriverLocation>> Handle(
        GetDeliveryLocationQuery query,
        CancellationToken cancellationToken = default)
    {
        var location = await driverLocationRepository.FindByDeliveryIdAsync(query.DeliveryId, cancellationToken);

        if (location is null)
            return Result<DriverLocation>.Failure(
                DeliveryTrackingErrors.LocationNotFound,
                $"No location data found for delivery with id {query.DeliveryId}.");

        return Result<DriverLocation>.Success(location);
    }
}