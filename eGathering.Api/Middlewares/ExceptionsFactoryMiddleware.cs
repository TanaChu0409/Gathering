using System.Net;

namespace eGathering.Api.Middlewares;

public class ExceptionsFactoryMiddleware : IMiddleware
{
    private static readonly Action<ILogger, Exception> _failureLogger =
        LoggerMessage.Define(LogLevel.Error, new(500, "Exception"), "Exception occurred");

    private readonly ILogger<ExceptionsFactoryMiddleware> _logger;

    public ExceptionsFactoryMiddleware(ILogger<ExceptionsFactoryMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context)
                    .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex)
                    .ConfigureAwait(false);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _failureLogger(_logger, ex);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        return context.Response
                        .WriteAsync($"{context.Response.StatusCode} Internal Server Error from the exception middleware. Please checking log.");
    }
}