namespace eGathering.Domain.Exceptions.Members;

internal sealed class MemberEmailDuplicatedException : DomainException
{
    public MemberEmailDuplicatedException(string message)
        : base(message)
    {
    }

    public MemberEmailDuplicatedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public MemberEmailDuplicatedException()
        : base(string.Empty)
    {
    }
}