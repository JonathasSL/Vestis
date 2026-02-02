using FluentValidation;
using Vestis._02_Application.CQRS.Studio.Commands;

namespace Vestis._02_Application.CQRS.Studio.Validators;

public class CreateStudioCommandValidator : AbstractValidator<CreateStudioCommand>
{
    private const int _StudioMaxLength = 256;
    private const int _EmailMaxLength = 256;

    public CreateStudioCommandValidator()
    {
        UserRules();
        NameRules();
        ContactEmailRules();
        PhoneNumberRules();
    }

    private void UserRules()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .Must(userId => userId != Guid.Empty)
            .WithMessage("User ID must be a valid GUID.");
    }

    private void NameRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(_StudioMaxLength)
            .WithMessage($"Name must not exceed {_StudioMaxLength} characters.");
    }

    private void ContactEmailRules()
    {
        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .WithMessage("Contact email must be a valid email address.")
            .MaximumLength(_EmailMaxLength)
            .WithMessage($"Contact email must not exceed {_EmailMaxLength} characters.");
    }

    private void PhoneNumberRules()
    {
        var characterLimit = 15;
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be a valid international format.")
            .MaximumLength(characterLimit)
            .WithMessage($"Phone number must not exceed {characterLimit} characters.");
    }
}
