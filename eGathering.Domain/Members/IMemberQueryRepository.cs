using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Members;

public interface IMemberQueryRepository : IRepository
{
    Task<IReadOnlyList<Member>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<Member>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Member>> GetByConditions(string? firstName, string? lastName, string? email, CancellationToken cancellationToken);
}