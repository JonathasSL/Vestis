using FluentValidation;
using Vestis._02_Application.CQRS.Studio.Commands;

namespace Vestis._02_Application.CQRS.Studio.Validators;

public class CreateStudioCommandValidator : AbstractValidator<CreateStudioCommand>
{
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
        var characterLimit = 256;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(characterLimit)
            .WithMessage($"Name must not exceed {characterLimit} characters.");
    }

    private void ContactEmailRules()
    {
        var characterLimit = 100;
        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .WithMessage("Contact email must be a valid email address.")
            .MaximumLength(characterLimit)
            .WithMessage($"Contact email must not exceed {characterLimit} characters.");
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
