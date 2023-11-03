using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Invitations;

public interface IInvitationCommandRepository : IRepository
{
    Task<Guid> CreateAsync(Invitation invitation, CancellationToken cancellationToken);
}