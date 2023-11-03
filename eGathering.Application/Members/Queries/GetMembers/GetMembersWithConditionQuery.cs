using eGathering.Application.Abstractions.Messaging;
using eGathering.Application.Members.Queries.Dto;

namespace eGathering.Application.Members.Queries.GetMembers;

public sealed record GetMembersWithConditionQuery(string? FirstName, string? LastName, string? Email) : IQuery<IReadOnlyList<MemberDto>>;