using MediatR;

namespace eGathering.Application.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(Guid Id, string FirstName, string LastName, string Email) : IRequest<Unit>;