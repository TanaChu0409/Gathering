using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eGathering.Persistence.Repositories.Queries;

public class MemberQueryRepository : IMemberQueryRepository
{
    private readonly GatheringContext _gatheringContext;

    public MemberQueryRepository(GatheringContext gatheringContext)
    {
        _gatheringContext = gatheringContext;
    }

    public IUnitOfWork UnitOfWork => _gatheringContext;

    public async Task<IReadOnlyList<Member>> GetAllAsync(CancellationToken cancellationToken)
    {
        var members = await _gatheringContext.Members
                                             .AsNoTracking()
                                             .ToListAsync(cancellationToken)
                                             .ConfigureAwait(false);
        return members;
    }

    public async Task<IReadOnlyList<Member>> GetByConditions(string? firstName, string? lastName, string? email, CancellationToken cancellationToken)
    {
        var members = await _gatheringContext.Members
                                             .Where(QueryCondition(firstName, lastName, email))
                                             .ToListAsync(cancellationToken)
                                             .ConfigureAwait(false);

        return members;
    }

    public async Task<IReadOnlyList<Member>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var members = await _gatheringContext.Members
                                             .Where(x => ids.Contains(x.Id))
                                             .AsNoTracking()
                                             .ToListAsync(cancellationToken)
                                             .ConfigureAwait(false);
        return members;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var member = await _gatheringContext.Members
                                            .FirstOrDefaultAsync(
                                                x => x.Id == id,
                                                cancellationToken)
                                            .ConfigureAwait(false);
        return member;
    }

#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high

    private static Expression<Func<Member, bool>> QueryCondition(string? firstName, string? lastName, string? email)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
    {
        if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(email))
        {
            return x =>
                        x.FullName.FirstName.Contains(firstName) &&
                        x.FullName.LastName.Contains(lastName) &&
                        x.Email.Value.Contains(email);
        }
        else if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
        {
            return x =>
                        x.FullName.FirstName.Contains(firstName) &&
                        x.FullName.LastName.Contains(lastName);
        }
        else if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(email))
        {
            return x =>
                        x.FullName.FirstName.Contains(firstName) &&
                        x.Email.Value.Contains(email);
        }
        else if (!string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(email))
        {
            return x =>
                        x.FullName.LastName.Contains(lastName) &&
                        x.Email.Value.Contains(email);
        }
        else if (!string.IsNullOrWhiteSpace(firstName))
        {
            return x =>
                        x.FullName.FirstName.Contains(firstName);
        }
        else if (!string.IsNullOrWhiteSpace(lastName))
        {
            return x =>
                        x.FullName.LastName.Contains(lastName);
        }
        else if (!string.IsNullOrWhiteSpace(email))
        {
            return x =>
                        x.Email.Value.Contains(email);
        }
        else
        {
            return x => true;
        }
    }
}