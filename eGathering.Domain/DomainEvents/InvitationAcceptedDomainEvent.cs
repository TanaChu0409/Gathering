using eGathering.Domain.SeedWork;

namespace eGathering.Domain.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent;