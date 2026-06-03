using Scripters.Regula.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;


public static class MiddlewareExtensions
{
    
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}