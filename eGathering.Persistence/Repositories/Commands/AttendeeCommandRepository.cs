using eGathering.Domain.Attendees;
using eGathering.Domain.SeedWork;

namespace eGathering.Persistence.Repositories.Commands;

public class AttendeeCommandRepository : IAttendeeCommandRepository
{
    private readonly GatheringContext _gatheringContext;

    public AttendeeCommandRepository(GatheringContext gatheringContext)
    {
        ArgumentNullException.ThrowIfNull(gatheringContext);
        _gatheringContext = gatheringContext;
    }

    public IUnitOfWork UnitOfWork => _gatheringContext;

    public async Task CreateAsync(Attendee attendee, CancellationToken cancellationToken)
    {
        await _gatheringContext.Set<Attendee>()
                               .AddAsync(attendee, cancellationToken)
                               .ConfigureAwait(false);
    }
}