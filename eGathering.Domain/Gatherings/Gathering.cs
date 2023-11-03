using eGathering.Domain.Attendees;
using eGathering.Domain.DomainEvents;
using eGathering.Domain.Errors;
using eGathering.Domain.Exceptions.Gatherings;
using eGathering.Domain.Invitations;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using eGathering.Domain.Shared;
using System.Globalization;

namespace eGathering.Domain.Gatherings;

public sealed class Gathering : AggregateRoot
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

    public Gathering(Guid id)
        : base(id)
    {
    }

    private Gathering(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string? location)
        : this(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
        CreatorId = creator.Id;
    }

    public Guid CreatorId { get; set; }

    public Member Creator { get; private set; }

    public GatheringType Type { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string Name { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Gathering Create(
        Guid id,
        Member creator,
        int type,
        DateTime scheduledAtUtc,
        string name,
        string? location,
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        if (!Enum.TryParse<GatheringType>(type.ToString(CultureInfo.InvariantCulture), out var gatherType))
        {
            throw new ArgumentOutOfRangeException(nameof(type));
        }

        // Create gathering
        var gathering = new Gathering(id, creator, gatherType, scheduledAtUtc, name, location);

        // Calculate gathering type details
        gathering.CalculateGatheringTypeDetails(maximumNumberOfAttendees, invitationsValidBeforeInHours);

        return gathering;
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        // Validate
        if (Creator.Id == member.Id)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.AlreadyPassed);
        }

        // Create invitation
        var invitation = new Invitation(Guid.NewGuid(), member, this);
        _invitations.Add(invitation);
        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        // Check if expired
        var isExpired = (Type == GatheringType.WithFixedNumberOfAttendess &&
                         NumberOfAttendees == MaximumNumberOfAttendees) ||
                        (Type == GatheringType.WithExpirationForInvitations &&
                         InvitationsExpireUtc < DateTime.UtcNow);
        if (isExpired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept();

        RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id, Id));

        _attendees.Add(attendee);
        NumberOfAttendees++;
        return attendee;
    }

    private void CalculateGatheringTypeDetails(int? maximumNumberOfAttendees, int? invitationsValidBeforeInHours)
    {
        switch (Type)
        {
            case GatheringType.WithFixedNumberOfAttendess:
                if (maximumNumberOfAttendees is null)
                {
                    throw new GatheringMaximumNumberOfAttendeesIsNullDomainException($"{nameof(maximumNumberOfAttendees)} can't be null");
                }

                MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;

            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new GatheringInvitationsValidBeforeHoursIsNullDomainException($"{nameof(invitationsValidBeforeInHours)} can't be null");
                }

                InvitationsExpireUtc = ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType), $"{nameof(GatheringType)} is out of range");
        }
    }
}