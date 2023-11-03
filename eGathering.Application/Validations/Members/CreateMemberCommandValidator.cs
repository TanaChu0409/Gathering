using eGathering.Application.Members.Commands.CreateMember;
using eGathering.Domain.ValueObjects;
using FluentValidation;

namespace eGathering.Application.Validations.Members;

public sealed class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(rule => rule.FirstName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(rule => rule.LastName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(rule => rule.Email).NotEmpty().MaximumLength(Email.MaxLength);
    }
}