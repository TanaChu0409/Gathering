using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using eGathering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace eGathering.Persistence.Repositories.Commands;

public class MemberCommandRepository : IMemberCommandRepository
{
    private readonly GatheringContext _gatheringContext;

    public MemberCommandRepository(GatheringContext gatheringContext)
    {
        ArgumentNullException.ThrowIfNull(gatheringContext);
        _gatheringContext = gatheringContext;
    }

    public IUnitOfWork UnitOfWork => _gatheringContext;

    public async Task<Guid> CreateAsync(Member member, CancellationToken cancellationToken)
    {
        var result = await _gatheringContext.Set<Member>()
                                            .AddAsync(member, cancellationToken)
                                            .ConfigureAwait(false);
        return result.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return false;
        }

        var member = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (member == null)
        {
            return false;
        }

        _gatheringContext.Set<Member>().Remove(member);
        return true;
    }

    public async Task<Member?> GetByFullNameForValidateAsync(FullName fullName, CancellationToken cancellationToken)
    {
        var member = await _gatheringContext.Set<Member>()
                                            .FirstOrDefaultAsync(
                                                    x =>
                                                            x.FullName.FirstName == fullName.FirstName &&
                                                            x.FullName.LastName == fullName.LastName,
                                                    cancellationToken)
                                            .ConfigureAwait(false);
        return member;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var member = await _gatheringContext.Set<Member>()
                                            .FirstOrDefaultAsync(
                                                x =>
                                                    x.Id == id,
                                                cancellationToken)
                                            .ConfigureAwait(false);
        return member;
    }

    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken)
    {
        return !await _gatheringContext.Set<Member>()
                                       .AnyAsync(
                                            x =>
                                                x.Email.Value == value.Value,
                                            cancellationToken)
                                       .ConfigureAwait(false);
    }

    public bool Update(Member member)
    {
        _gatheringContext.Set<Member>().Update(member);
        return true;
    }
}