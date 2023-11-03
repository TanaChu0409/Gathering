using eGathering.Domain.Gatherings;
using eGathering.Domain.Invitations;
using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Attendees;

public sealed class Attendee : Entity
{
    public Attendee(Guid id)
        : base(id)
    {
    }

    internal Attendee(Invitation invitation)
            : this(Guid.NewGuid())
    {
        MemberId = invitation.MemberId;
        GatheringId = invitation.GatheringId;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid MemberId { get; private set; }

    public Guid GatheringId { get; private set; }

    public Gathering Gathering { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}