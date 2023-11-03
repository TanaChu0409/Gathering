using eGathering.Domain.Attendees;
using eGathering.Domain.Gatherings;
using eGathering.Domain.Invitations;
using MediatR;

namespace eGathering.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Unit>
{
    private readonly IGatheringCommandRepository _gatheringRepository;
    private readonly IAttendeeCommandRepository _attendeeRepository;

    public AcceptInvitationCommandHandler(
        IGatheringCommandRepository gatheringRepository,
        IAttendeeCommandRepository attendeeRepository)
    {
        _gatheringRepository = gatheringRepository;
        _attendeeRepository = attendeeRepository;
    }

    public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdAsync(request.GatheringId, cancellationToken).ConfigureAwait(false);
        if (gathering is null)
        {
            return Unit.Value;
        }

        var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == request.InvitationId);
        if (invitation is null || invitation.Status != InvitationStatus.Pending)
        {
            return Unit.Value;
        }

        var attendee = gathering.AcceptInvitation(invitation);
        if (attendee is not null)
        {
            await _attendeeRepository.CreateAsync(attendee, cancellationToken).ConfigureAwait(false);
        }

        await _attendeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}