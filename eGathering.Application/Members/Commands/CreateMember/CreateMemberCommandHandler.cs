using eGathering.Application.Abstractions.Messaging;
using eGathering.Domain.Members;
using eGathering.Domain.Shared;
using eGathering.Domain.ValueObjects;

namespace eGathering.Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand>
{
    private readonly IMemberCommandRepository _memberRepository;

    public CreateMemberCommandHandler(IMemberCommandRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FirstName, request.LastName);
        var email = Email.Create(request.Email);
        if (email.IsFailure)
        {
            return Result.Failure(email.Error);
        }

        var newMemberResult = Member.Create(
                                fullName,
                                email.Value,
                                await _memberRepository.IsEmailUniqueAsync(email.Value, cancellationToken).ConfigureAwait(false));

        if (newMemberResult.IsFailure)
        {
            return newMemberResult;
        }

        _ = await _memberRepository.CreateAsync(newMemberResult.Value, cancellationToken).ConfigureAwait(false);
        await _memberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Success();
    }
}