using System.Net;

namespace eGathering.Api.Middlewares;

public class ExceptionsMiddleware
{
    private static readonly Action<ILogger, Exception> _failureLogger =
        LoggerMessage.Define(LogLevel.Error, new(500, "Exception"), "Exception occurred");

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsMiddleware> _logger;

    public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _failureLogger(_logger, ex);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync($"{context.Response.StatusCode} Internal Server Error from the exception middleware. Please checking log.");
    }
}