using eGathering.Application.Abstractions.Messaging;

namespace eGathering.Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string FirstName,
    string LastName,
    string Email) : ICommand;