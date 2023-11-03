namespace eGathering.Domain.Exceptions.Gatherings;

internal sealed class GatheringInvitationsValidBeforeHoursIsNullDomainException : DomainException
{
    public GatheringInvitationsValidBeforeHoursIsNullDomainException(string message)
        : base(message)
    {
    }

    public GatheringInvitationsValidBeforeHoursIsNullDomainException()
        : base(string.Empty)
    {
    }

    public GatheringInvitationsValidBeforeHoursIsNullDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}