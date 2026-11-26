using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Vestis._02_Application.CQRS.Client.Commands;

namespace Vestis._02_Application.CQRS.Client.Validators;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private const int _MaxNameLength = 256;
    private const int _MaxEmailLength = 256;
    private const int _MaxPhoneNumberLength = 15;

    public CreateClientCommandValidator()
    {
        NameRules();
        EmailRules();
        PhoneNumberRules();
    }

    private void NameRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(_MaxNameLength).WithMessage($"Name must not exceed {_MaxNameLength} characters.");
    }

    private void EmailRules()
    {
        RuleFor(x => x.Email)
            //.NotEmpty().WithMessage("Email is required.")
            .MaximumLength(_MaxEmailLength).WithMessage($"Email must not exceed {_MaxEmailLength} characters.")
            .EmailAddress().WithMessage("Email must be a valid email address.");
    }

    private void PhoneNumberRules()
    {
        RuleFor(x => x.PhoneNumber)
            //.NotEmpty().WithMessage("Phone number is required.")
            //.Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international format.")
            .MaximumLength(_MaxPhoneNumberLength).WithMessage($"Phone number must not exceed {_MaxPhoneNumberLength} characters.");
    }
}
