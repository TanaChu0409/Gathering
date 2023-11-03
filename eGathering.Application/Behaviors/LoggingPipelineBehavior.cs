using eGathering.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eGathering.Application.Behaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    private static readonly Action<ILogger, string, TRequest, DateTime, Exception?> _pipelineLogger =
        LoggerMessage.Define<string, TRequest, DateTime>(
            LogLevel.Information,
            new(0, typeof(TRequest).Name),
            "{@Behavior} request {@RequestName}, {@DateTimeUtc}");

    private static readonly Action<ILogger, TRequest, Error, DateTime, Exception?> _errorLogger =
        LoggerMessage.Define<TRequest, Error, DateTime>(
            LogLevel.Error,
            new(-1, typeof(TRequest).Name),
            "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}");

    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _pipelineLogger(_logger, "Starting", request, DateTime.UtcNow, null);

        var result = await next().ConfigureAwait(false);

        if (result.IsFailure)
        {
            _errorLogger(_logger, request, result.Error, DateTime.UtcNow, null);
        }

        _pipelineLogger(_logger, "Completed", request, DateTime.UtcNow, null);

        return result;
    }
}