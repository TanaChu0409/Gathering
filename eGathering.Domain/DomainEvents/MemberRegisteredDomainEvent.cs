using eGathering.Domain.SeedWork;

namespace eGathering.Domain.DomainEvents;
public sealed record MemberRegisteredDomainEvent(Guid MemberId) : IDomainEvent;