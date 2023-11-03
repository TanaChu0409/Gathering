using eGathering.Application.Abstractions.Messaging;

namespace eGathering.Application.Invitations.Commands.SendInvitation;
public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : ICommand;