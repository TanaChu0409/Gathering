using eGathering.Domain.Invitations;
using eGathering.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace eGathering.Persistence.Repositories.Commands;

public class InvitationCommandRepository : IInvitationCommandRepository
{
    private readonly GatheringContext _gatheringContext;

    public InvitationCommandRepository(GatheringContext gatheringContext)
    {
        ArgumentNullException.ThrowIfNull(gatheringContext);
        _gatheringContext = gatheringContext;
    }

    public IUnitOfWork UnitOfWork => _gatheringContext;

    public async Task<Guid> CreateAsync(Invitation invitation, CancellationToken cancellationToken)
    {
        var result = await _gatheringContext.Set<Invitation>()
                                            .AddAsync(invitation, cancellationToken)
                                            .ConfigureAwait(false);
        return result.Entity.Id;
    }

    public async Task<Invitation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var invitation = await _gatheringContext.Set<Invitation>()
                                                .FirstOrDefaultAsync(
                                                    x => x.Id == id,
                                                    cancellationToken)
                                                .ConfigureAwait(false);
        return invitation;
    }
}