using FluentValidation;
using Vestis._02_Application.Address.Commands;

namespace Vestis._02_Application.Address.Validators;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        ValidateStreet();
        ValidateNumber();
        ValidateNeighborhood();
        ValidateCity();
        ValidateState();
        ValidateZipCode();
    }

    void ValidateStreet()
    {
        var characterLimit = 100;
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("O campo Rua é obrigatório.")
            .MaximumLength(characterLimit).WithMessage($"O campo Rua deve ter no máximo {characterLimit} caracteres.");
    }

    void ValidateNumber()
    {
        var characterLimit = 10;
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("O campo Número é obrigatório.")
            .MaximumLength(characterLimit).WithMessage($"O campo Número deve ter no máximo {characterLimit} caracteres.");
    }

    void ValidateNeighborhood()
    {
        var characterLimit = 50;
        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage("O campo Bairro é obrigatório.")
            .MaximumLength(characterLimit).WithMessage($"O campo Bairro deve ter no máximo {characterLimit} caracteres.");
    }

    void ValidateCity()
    {
        var characterLimit = 50;
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("O campo Cidade é obrigatório.")
            .MaximumLength(characterLimit).WithMessage($"O campo Cidade deve ter no máximo {characterLimit} caracteres.");
    }

    void ValidateState()
    {
        var characterLimit = 50;
        RuleFor(x => x.State)
            .NotEmpty().WithMessage("O campo Estado é obrigatório.")
            .MaximumLength(characterLimit).WithMessage($"O campo Estado deve ter no máximo {characterLimit} caracteres.");
    }

    void ValidateZipCode()
    {
        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O campo CEP é obrigatório.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("O campo CEP deve estar no formato 12345-678.");
    }
}
