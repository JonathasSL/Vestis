using FluentValidation;
using Vestis._02_Application.CQRS.User.Commands;

namespace Vestis._02_Application.CQRS.User.Validators;

public class UserEmailVerificationCommandValidator : AbstractValidator<UserEmailVerificationCommand>
{
    private const int _EmailMaxLength = 256;
    private const int _CodeMaxLength = 256;

    public UserEmailVerificationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.")
            .MaximumLength(_EmailMaxLength).WithMessage($"Email deve ter no máximo {_EmailMaxLength} caracteres.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Código é obrigatório.")
            .MaximumLength(_CodeMaxLength).WithMessage($"Código deve ter no máximo {_CodeMaxLength} caracteres.");
    }
}
