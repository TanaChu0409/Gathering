namespace eGathering.Api.Controllers.Gatherings;
public record GatheringRequest(
    int Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours
    );