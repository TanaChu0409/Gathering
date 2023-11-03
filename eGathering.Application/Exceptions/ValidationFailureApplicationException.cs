using eGathering.Domain.Exceptions;

namespace eGathering.Application.Exceptions;

internal sealed class ValidationFailureApplicationException : DomainException
{
    public ValidationFailureApplicationException(string message)
        : base(message)
    {
    }

    public ValidationFailureApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ValidationFailureApplicationException()
        : base(string.Empty)
    {
    }
}