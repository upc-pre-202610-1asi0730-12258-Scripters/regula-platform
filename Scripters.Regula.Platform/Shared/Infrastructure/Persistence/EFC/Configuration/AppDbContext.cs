using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.CommercialManagement.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyCommercialManagementConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}