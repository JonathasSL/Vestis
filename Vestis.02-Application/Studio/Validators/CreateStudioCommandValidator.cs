using FluentValidation;
using Vestis._02_Application.Studio.Commands;

namespace Vestis._02_Application.Studio.Validators;

public class CreateStudioCommandValidator : AbstractValidator<CreateStudioCommand>
{
    public CreateStudioCommandValidator()
    {
        NameRules();
        ContactEmailRules();
        PhoneNumberRules();
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
