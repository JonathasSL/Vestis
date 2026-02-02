using FluentValidation;
using Vestis._02_Application.CQRS.User.Commands;

namespace Vestis._02_Application.CQRS.User.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const int _NameMaxLength = 256;
    private const int _EmailMaxLength = 256;

    public CreateUserCommandValidator()
    {
        NameRules();
        EmailRules();
        PasswordRules();
    }
    private void NameRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(_NameMaxLength).WithMessage($"Name must not exceed {_NameMaxLength} characters.");
    }
    private void EmailRules()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(_EmailMaxLength).WithMessage($"Email must not exceed {_EmailMaxLength} characters.");
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
