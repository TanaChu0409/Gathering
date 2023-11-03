using eGathering.Domain.Members;
using eGathering.Domain.ValueObjects;
using MediatR;

namespace eGathering.Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Unit>
{
    private readonly IMemberCommandRepository _memberCommandRepository;

    public UpdateMemberCommandHandler(IMemberCommandRepository memberCommandRepository)
    {
        _memberCommandRepository = memberCommandRepository;
    }

    public async Task<Unit> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberCommandRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        if (member is null)
        {
            return Unit.Value;
        }

        var fullName = FullName.Create(request.FirstName, request.LastName);
        var email = Email.Create(request.Email);

        if (email.IsFailure)
        {
            return Unit.Value;
        }

        member.Update(fullName, email.Value);
        _memberCommandRepository.Update(member);
        await _memberCommandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }
}