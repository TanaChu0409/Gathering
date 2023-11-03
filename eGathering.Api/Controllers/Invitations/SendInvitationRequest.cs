namespace eGathering.Api.Controllers.Invitations;
public record SendInvitationRequest(Guid MemberId, Guid GatheringId);