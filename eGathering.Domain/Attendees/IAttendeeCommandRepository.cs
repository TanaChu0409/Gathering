using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Attendees;

public interface IAttendeeCommandRepository : IRepository
{
    Task CreateAsync(Attendee attendee, CancellationToken cancellationToken);
}