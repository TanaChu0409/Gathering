using eGathering.Domain.DomainEvents;
using eGathering.Domain.Errors;
using eGathering.Domain.Gatherings;
using eGathering.Domain.SeedWork;
using eGathering.Domain.Shared;
using eGathering.Domain.ValueObjects;

namespace eGathering.Domain.Members;

public sealed class Member : AggregateRoot
{
    private readonly List<Gathering> _gatherings = new();

    public Member(Guid id)
        : base(id)
    {
    }

    private Member(Guid id, FullName fullName, Email email)
        : this(id)
    {
        FullName = fullName;
        Email = email;
        CreateOnUtc = DateTime.UtcNow;
    }

    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public DateTime CreateOnUtc { get; private set; }

    public IReadOnlyCollection<Gathering> Gatherings => _gatherings;

    public static Result<Member> Create(FullName fullName, Email email, bool isEmailUnique)
    {
        if (!isEmailUnique)
        {
            return Result.Failure<Member>(DomainErrors.Member.EmailIsNotUnique);
        }

        var member = new Member(Guid.NewGuid(), fullName, email);
        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));
        return member;
    }

    public void Update(FullName fullName, Email email)
    {
        FullName = fullName;
        Email = email;
    }
}