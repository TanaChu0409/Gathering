using eGathering.Domain.Attendees;
using eGathering.Domain.Gatherings;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;

namespace eGathering.Domain.Invitations;

public sealed class Invitation : Entity
{
    private Invitation()
    {
    }

    internal Invitation(
            Guid id,
            Member member,
            Gathering gathering)
            : base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreateOnUtc = DateTime.UtcNow;
    }

    public Guid MemberId { get; private set; }

    public Guid GatheringId { get; private set; }

    public Gathering Gathering { get; private set; }

    public InvitationStatus Status { get; private set; }

    public DateTime CreateOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    internal Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        ModifiedOnUtc = DateTime.UtcNow;

        //  Create attendee
        var attendee = new Attendee(this);
        return attendee;
    }

    internal void Expire()
    {
        Status = InvitationStatus.Expired;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}