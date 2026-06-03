using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scripters.Regula.Platform.Resources.Errors;
using Scripters.Regula.Platform.Resources.Shared;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;


public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer)
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly IStringLocalizer<CommonMessages> _commonLocalizer = commonLocalizer;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogWarning(exception, "Request was cancelled: {Message}", exception.Message);
            await HandleOperationCanceledExceptionAsync(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
            await HandleGenericExceptionAsync(context);
        }
    }


    private async Task HandleOperationCanceledExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = StatusCodes.Status409Conflict;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = _errorLocalizer["OperationCancelled"],
            Detail = _errorLocalizer["OperationCancelled"],
            Instance = context.Request.Path
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = JsonSerializer.Serialize(problemDetails, jsonOptions);

        await context.Response.WriteAsync(result);
    }

    private async Task HandleGenericExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = _commonLocalizer["InternalServerError"],
            Detail = _errorLocalizer["GenericError"],
            Instance = context.Request.Path
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = JsonSerializer.Serialize(problemDetails, jsonOptions);

        await context.Response.WriteAsync(result);
    }
}