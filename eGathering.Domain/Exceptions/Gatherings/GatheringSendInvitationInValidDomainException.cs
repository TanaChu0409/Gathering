namespace eGathering.Domain.Exceptions.Gatherings;

internal sealed class GatheringSendInvitationInValidDomainException : DomainException
{
    public GatheringSendInvitationInValidDomainException(string message)
        : base(message)
    {
    }

    public GatheringSendInvitationInValidDomainException()
        : base(string.Empty)
    {
    }

    public GatheringSendInvitationInValidDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}