using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.UseSnakeCaseNamingConvention();
    }
}