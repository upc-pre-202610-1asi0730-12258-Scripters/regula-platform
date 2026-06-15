using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Scripters.Regula.Platform.CommercialManagement.Infrastructure.Persistence.EFC.Repositories;

public class CommercialDebtPaymentRepository(AppDbContext context)
    : BaseRepository<CommercialDebtPayment>(context), ICommercialDebtPaymentRepository
{
}