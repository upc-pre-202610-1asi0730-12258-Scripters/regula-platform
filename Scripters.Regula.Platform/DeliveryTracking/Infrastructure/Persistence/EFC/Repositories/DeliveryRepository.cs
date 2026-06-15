using Scripters.Regula.Platform.DeliveryTracking.Domain.Model.Aggregates;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Scripters.Regula.Platform.DeliveryTracking.Infrastructure.Persistence.EFC.Repositories;

public class DeliveryRepository(AppDbContext context)
    : BaseRepository<Delivery>(context), IDeliveryRepository
{
}
