using CSupporter.Application.Exceptions;
using CSupporter.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CSupporter.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                """
                Unhandled exception:
                Message:     {Message}
                InnerExc:    {InnerException}
                Path:        {Path}
                Method:      {Method}
                Query:       {Query}
                User:        {User}
                IP:          {IP}
                """,
                ex.Message,
                ex.InnerException?.ToString() ?? string.Empty,
                context.Request.Path,
                context.Request.Method,
                context.Request.QueryString,
                context.User?.Identity?.Name ?? "Anonymous",
                context.Connection.RemoteIpAddress?.ToString()
            );

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        var details = new ProblemDetails();

        switch (exception)
        {
            case ArgumentException:
                statusCode = StatusCodes.Status400BadRequest;
                break;

            case EntityNotFoundException:
            case AggregateException:
            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                break;

            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status401Unauthorized;
                break;

            case ValidationException validationEx:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                details.Extensions["errors"] = validationEx.Errors;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var result = ApiResult<ProblemDetails>.Error(details, exception.Message, statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        await context.Response.WriteAsync(json);
    }
}
