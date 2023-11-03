using eGathering.Domain.DomainEvents;
using eGathering.Domain.Gatherings;
using eGathering.Domain.SeedWork;
using MediatR;

namespace eGathering.Application.Invitations.Events;

internal sealed class InvitationAcceptedDomainEventHandler
    : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatheringCommandRepository _gatheringCommandRepository;

    public InvitationAcceptedDomainEventHandler(IEmailService emailService, IGatheringCommandRepository gatheringCommandRepository)
    {
        _emailService = emailService;
        _gatheringCommandRepository = gatheringCommandRepository;
    }

    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringCommandRepository.GetByIdAsync(notification.GatheringId, cancellationToken).ConfigureAwait(false);
        if (gathering is null)
        {
            return;
        }

        var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == notification.InvitationId);
        if (invitation is null)
        {
            return;
        }

        // Send mail
        await _emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken).ConfigureAwait(false);
    }
}