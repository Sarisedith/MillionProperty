using System.Diagnostics;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task Invoke(HttpContext context)
    {
        var correlationId = Activity.Current?.Id ?? context.TraceIdentifier;
        context.Response.Headers["X-Correlation-Id"] = correlationId;

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["Path"] = context.Request.Path
        }))
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex, correlationId);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized", correlationId);
            }
            catch (NotImplementedException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotImplemented, "Not implemented", correlationId);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Unexpected error", correlationId);
            }
        }
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex, string correlationId)
    {
        _logger.LogWarning(ex, "Validation error {CorrelationId}", correlationId);

        var problem = new ValidationProblemDetails(
            ex.Errors
              .GroupBy(e => e.PropertyName)
              .ToDictionary(
                  g => g.Key,
                  g => g.Select(e => e.ErrorMessage).ToArray()))
        {
            Title = "One or more validation errors occurred.",
            Status = (int)HttpStatusCode.BadRequest,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = correlationId;

        await WriteProblemDetailsAsync(context, problem);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string title, string correlationId)
    {
        _logger.LogError(ex, "{Title} {CorrelationId}", title, correlationId);

        var problem = new ProblemDetails
        {
            Title = title,
            Status = (int)statusCode,
            Instance = context.Request.Path,
            Detail = ex.Message
        };

        problem.Extensions["traceId"] = correlationId;

        await WriteProblemDetailsAsync(context, problem);
    }

    private async Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails problem)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, _serializerOptions));
    }
}
