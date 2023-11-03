using eGathering.Application.Abstractions.Messaging;
using eGathering.Application.Members.Queries.Dto;
using eGathering.Domain.Members;
using eGathering.Domain.Shared;

namespace eGathering.Application.Members.Queries.GetMembers;

internal sealed class GetMembersWithConditionQueryHandler : IQueryHandler<GetMembersWithConditionQuery, IReadOnlyList<MemberDto>>
{
    private readonly IMemberQueryRepository _memberQueryRepository;

    public GetMembersWithConditionQueryHandler(IMemberQueryRepository memberQueryRepository)
    {
        _memberQueryRepository = memberQueryRepository;
    }

    public async Task<Result<IReadOnlyList<MemberDto>>> Handle(GetMembersWithConditionQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberQueryRepository.GetByConditions(request.FirstName, request.LastName, request.Email, cancellationToken).ConfigureAwait(false);
        return members.Select(x => new MemberDto(
            x.Id,
            x.FullName.FirstName,
            x.FullName.LastName,
            x.Email.Value)).ToList();
    }
}