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
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");
    }

    private void ContactEmailRules()
    {
        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .WithMessage("Contact email must be a valid email address.")
            .MaximumLength(100)
            .WithMessage("Contact email must not exceed 100 characters.");
    }

    private void PhoneNumberRules()
    {
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be a valid international format.")
            .MaximumLength(15)
            .WithMessage("Phone number must not exceed 15 characters.");
    }
}
