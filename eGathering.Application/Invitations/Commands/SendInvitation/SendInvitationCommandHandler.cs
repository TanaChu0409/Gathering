using eGathering.Application.Abstractions.Messaging;
using eGathering.Domain.Gatherings;
using eGathering.Domain.Invitations;
using eGathering.Domain.Members;
using eGathering.Domain.Shared;

namespace eGathering.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : ICommandHandler<SendInvitationCommand>
{
    private readonly IMemberCommandRepository _memberRepository;
    private readonly IGatheringCommandRepository _gatheringRepository;
    private readonly IInvitationCommandRepository _invitationRepository;

    public SendInvitationCommandHandler(IMemberCommandRepository memberRepository, IGatheringCommandRepository gatheringRepository, IInvitationCommandRepository invitationRepository)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _invitationRepository = invitationRepository;
    }

    public async Task<Result> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken).ConfigureAwait(false);
        var gathering = await _gatheringRepository.GetByIdAsync(request.GatheringId, cancellationToken).ConfigureAwait(false);
        if (member is null || gathering is null)
        {
            return Result.Failure(new Error(
                "Gathering.SendInvitationHandler",
                $"Member id or gathering id isn't find"));
        }

        var invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            return invitationResult;
        }

        _ = await _invitationRepository.CreateAsync(invitationResult.Value, cancellationToken).ConfigureAwait(false);
        await _invitationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}