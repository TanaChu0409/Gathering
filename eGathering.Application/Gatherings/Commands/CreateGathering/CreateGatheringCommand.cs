using MediatR;

namespace eGathering.Application.Gatherings.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
    Guid MemberId,
    int Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours
    ) : IRequest<Unit>;