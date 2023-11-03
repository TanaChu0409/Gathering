using MediatR;

namespace eGathering.Application.Invitations.Commands.AcceptInvitation;
public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : IRequest<Unit>;