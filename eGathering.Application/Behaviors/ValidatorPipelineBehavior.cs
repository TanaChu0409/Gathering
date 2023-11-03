using eGathering.Application.Extensions;
using eGathering.Domain.Shared;
using FluentValidation;
using MediatR;

namespace eGathering.Application.Behaviors;

public sealed class ValidatorPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        var typeName = request.GetGenericTypeName();
        var failures = _validators
                       .Select(validator => validator.Validate(request))
                       .SelectMany(validateResult => validateResult.Errors)
                       .Where(validateError => validateError is not null)
                       .ToList();
        if (failures.Any())
        {
            return (TResponse)Result.Failure(new Error(
                typeName,
                new ValidationException("Validation exception", failures).ToString()));
        }

        return await next().ConfigureAwait(false);
    }
}