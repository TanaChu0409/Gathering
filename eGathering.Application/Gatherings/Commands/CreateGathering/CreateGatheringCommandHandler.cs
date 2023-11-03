using eGathering.Domain.Gatherings;
using eGathering.Domain.Members;
using MediatR;

namespace eGathering.Application.Gatherings.Commands.CreateGathering;

internal sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand, Unit>
{
    private readonly IMemberCommandRepository _memberRepository;
    private readonly IGatheringCommandRepository _gatheringRepository;

    public CreateGatheringCommandHandler(IMemberCommandRepository memberRepository, IGatheringCommandRepository gatheringRepository)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
    }

    public async Task<Unit> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken).ConfigureAwait(false);

        if (member is null)
        {
            return Unit.Value;
        }

        var gathering = Gathering.Create(
            Guid.NewGuid(),
            member,
            request.Type,
            request.ScheduledAtUtc,
            request.Name,
            request.Location,
            request.MaximumNumberOfAttendees,
            request.InvitationsValidBeforeInHours);

        _ = await _gatheringRepository.CreateAsync(gathering, cancellationToken).ConfigureAwait(false);
        await _gatheringRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}