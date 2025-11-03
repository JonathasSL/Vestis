using FluentValidation;
using Vestis._02_Application.CQRS.User.Commands;

namespace Vestis._02_Application.CQRS.User.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        NameRules();
        EmailRules();
        PasswordRules();
    }
    private void NameRules()
    {
        var characterLimit = 256;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(characterLimit).WithMessage($"Name must not exceed {characterLimit} characters.");
    }
    private void EmailRules()
    {
        var characterLimit = 100;
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(characterLimit).WithMessage($"Email must not exceed {characterLimit} characters.");
    }
    private void PasswordRules()
    {
        var characterLimit = 32;
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(characterLimit).WithMessage($"Password must not exceed {characterLimit} characters.");
    }
}
