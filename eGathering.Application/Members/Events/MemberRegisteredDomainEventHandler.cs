using eGathering.Domain.DomainEvents;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using MediatR;

namespace eGathering.Application.Members.Events;

internal sealed class MemberRegisteredDomainEventHandler
    : INotificationHandler<MemberRegisteredDomainEvent>
{
    private readonly IMemberQueryRepository _memberQueryRepository;
    private readonly IEmailService _emailService;

    public MemberRegisteredDomainEventHandler(IMemberQueryRepository memberQueryRepository, IEmailService emailService)
    {
        _memberQueryRepository = memberQueryRepository;
        _emailService = emailService;
    }

    public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var member = await _memberQueryRepository.GetByIdAsync(notification.MemberId, cancellationToken).ConfigureAwait(false);
        if (member is null)
        {
            return;
        }

        await _emailService.SendWelcomeEmailAsync(member, cancellationToken).ConfigureAwait(false);
    }
}