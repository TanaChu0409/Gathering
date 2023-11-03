using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Gatherings;

public interface IGatheringCommandRepository : IRepository
{
    Task<IReadOnlyList<Gathering>> GetAllAsync(CancellationToken cancellationToken);

    Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Gathering>> GetByIdAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

    Task<Guid> CreateAsync(Gathering gathering, CancellationToken cancellationToken);

    Task<Gathering?> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Gathering>> GetByNameAsync(string name, CancellationToken cancellationToken);
}