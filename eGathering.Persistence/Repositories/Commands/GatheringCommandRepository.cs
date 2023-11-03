using eGathering.Domain.Gatherings;
using eGathering.Domain.SeedWork;
using eGathering.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace eGathering.Persistence.Repositories.Commands;

public class GatheringCommandRepository : IGatheringCommandRepository
{
    private readonly GatheringContext _gatheringContext;

    public GatheringCommandRepository(GatheringContext gatheringContext)
    {
        ArgumentNullException.ThrowIfNull(gatheringContext);
        _gatheringContext = gatheringContext;
    }

    public IUnitOfWork UnitOfWork => _gatheringContext;

    public async Task<Guid> CreateAsync(Gathering gathering, CancellationToken cancellationToken)
    {
        var result = await _gatheringContext.Set<Gathering>()
                                            .AddAsync(gathering, cancellationToken)
                                            .ConfigureAwait(false);
        return result.Entity.Id;
    }

    public async Task<IReadOnlyList<Gathering>> GetAllAsync(CancellationToken cancellationToken)
    {
        var gatherings = await _gatheringContext.Set<Gathering>()
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken)
                                                .ConfigureAwait(false);
        return gatherings;
    }

    public async Task<IReadOnlyList<Gathering>> GetByNameAsync(
        string name,
        CancellationToken cancellationToken) =>
        await ApplySpecification(new GatheringByNameSpecification(name))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

    public async Task<Gathering?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await ApplySpecification(new GatheringByIdSplitSpecification(id))
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

    public async Task<IReadOnlyList<Gathering>> GetByIdAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var gatherings = await _gatheringContext.Set<Gathering>()
                                                .Where(x =>
                                                    ids.Contains(x.Id))
                                                .ToListAsync(cancellationToken)
                                                .ConfigureAwait(false);
        return gatherings;
    }

    public async Task<Gathering?> GetByIdWithCreatorAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await ApplySpecification(new GatheringByIdWithCreatorSpecification(id))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

    private IQueryable<Gathering> ApplySpecification(
        Specification<Gathering> specification)
    {
        return SpecificationEvaluator.GetQuery(
            _gatheringContext.Set<Gathering>(),
            specification);
    }
}