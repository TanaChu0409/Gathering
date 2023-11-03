using eGathering.Application.Abstractions.Messaging;
using eGathering.Application.Members.Queries.Dto;

namespace eGathering.Application.Members.Queries.GetMemberDetail;

public sealed record GetMemberDetailQuery(Guid Id) : IQuery<MemberDto?>;