using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Application.Internal.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;
using Scripters.Regula.Platform.CommercialManagement.Infrastructure.Persistence.EFC.Repositories;
using Scripters.Regula.Platform.DeliveryTracking.Application.Internal.QueryServices;
using Scripters.Regula.Platform.DeliveryTracking.Application.QueryServices;
using Scripters.Regula.Platform.DeliveryTracking.Domain.Repositories;
using Scripters.Regula.Platform.DeliveryTracking.Infrastructure.Persistence.EFC.Repositories;
using Scripters.Regula.Platform.Iam.Application.Internal.CommandServices;
using Scripters.Regula.Platform.Iam.Application.Internal.OutboundServices;
using Scripters.Regula.Platform.Iam.Domain.Repositories;
using Scripters.Regula.Platform.Iam.Infrastructure.Hashing.BCrypt;
using Scripters.Regula.Platform.Iam.Infrastructure.Persistence.EFC.Repositories;
using Scripters.Regula.Platform.Iam.Infrastructure.Tokens.JWT;
using Cortex.Mediator.DependencyInjection;
using Scripters.Regula.Platform.InventoryManagement.Application.CommandServices;
using Scripters.Regula.Platform.InventoryManagement.Application.Internal.CommandServices;
using Scripters.Regula.Platform.InventoryManagement.Application.Internal.QueryServices;
using Scripters.Regula.Platform.InventoryManagement.Application.QueryServices;
using Scripters.Regula.Platform.InventoryManagement.Domain.Repositories;
using Scripters.Regula.Platform.InventoryManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Scripters.Regula.Platform.InventoryManagement.Resources;
using Scripters.Regula.Platform.Shared.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using Scripters.Regula.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Scripters.Regula.Platform.Shared.Infrastructure.Pipeline.Middleware.extensions;
using ProblemDetailsFactory = Scripters.Regula.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails services
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Database Context
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization
builder.Services.AddLocalization();

// Register IStringLocalizer for InventoryManagementMessages
builder.Services.AddSingleton<IStringLocalizer<InventoryManagementMessages>, StringLocalizer<InventoryManagementMessages>>();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// ── Shared Bounded Context ──────────────────────────────────────────────────
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ── InventoryManagement Bounded Context ────────────────────────────────────
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();
builder.Services.AddScoped<IInventoryQueryService, InventoryQueryService>();

// Cortex Mediator for InventoryManagement domain events
builder.Services.AddCortexMediator([typeof(Program)]);

// ── DeliveryTracking Bounded Context ───────────────────────────────────────
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDriverLocationRepository, DriverLocationRepository>();
builder.Services.AddScoped<IDeliveryLocationQueryService, DeliveryLocationQueryService>();

// ── CommercialManagement Bounded Context ───────────────────────────────────
builder.Services.AddScoped<ICommercialCustomerRepository, CommercialCustomerRepository>();
builder.Services.AddScoped<ICommercialDebtRepository, CommercialDebtRepository>();
builder.Services.AddScoped<ICustomerDebtCommandService, CustomerDebtCommandService>();
builder.Services.AddScoped<ICommercialDebtPaymentRepository, CommercialDebtPaymentRepository>();

// ── IAM Bounded Context ────────────────────────────────────────────────────
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configure Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"];

if (!string.IsNullOrWhiteSpace(jwtSecret))
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
            };
        });
}

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Scripters.Regula.Platform",
            Version = "v1",
            Description = "Scripters Regula Platform API",
            TermsOfService = new Uri("https://scripters.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Scripters Team",
                Email = "contact@scripters.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.EnableAnnotations();
});

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline
app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
