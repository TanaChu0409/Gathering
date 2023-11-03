using eGathering.Application.Abstractions.Messaging;
using eGathering.Application.Members.Queries.Dto;
using eGathering.Domain.Members;
using eGathering.Domain.Shared;

namespace eGathering.Application.Members.Queries.GetMemberDetail;

internal sealed class GetMemberDetailQueryHandler : IQueryHandler<GetMemberDetailQuery, MemberDto?>
{
    private readonly IMemberQueryRepository _memberQueryRepository;

    public GetMemberDetailQueryHandler(IMemberQueryRepository memberQueryRepository)
    {
        _memberQueryRepository = memberQueryRepository;
    }

    public async Task<Result<MemberDto?>> Handle(GetMemberDetailQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberQueryRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (member is null)
        {
            return Result.Failure<MemberDto?>(new Error(
                "Member.NotFound",
                $"The member with Id {request.Id} was not found"));
        }

        return new MemberDto(member.Id, member.FullName.FirstName, member.FullName.LastName, member.Email.Value);
    }
}