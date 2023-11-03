namespace eGathering.Domain.Exceptions.Gatherings;

internal sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainException
{
    public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message)
        : base(message)
    {
    }

    public GatheringMaximumNumberOfAttendeesIsNullDomainException()
        : base(string.Empty)
    {
    }

    public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}