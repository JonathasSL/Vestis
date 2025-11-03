using FluentValidation;
using Vestis._02_Application.CQRS.StudioMembership.Commands;

namespace Vestis._02_Application.CQRS.StudioMembership.Validators;

public class CreateStudioMembershipCommandValidation : AbstractValidator<CreateStudioMembershipCommand>
{
    public CreateStudioMembershipCommandValidation()
    {
        UserIdRules();
        StudioIdRules();
        UserRoleRules();
    }

    private void UserIdRules()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
    private void StudioIdRules()
    {
        RuleFor(x => x.StudioId)
            .NotEmpty()
            .WithMessage("Studio ID is required.");
    }
    private void UserRoleRules()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("User role is required.")
            .Must(role => role == "Member" || role == "Admin" || role == "Owner" || role == "Client")
            .WithMessage("User role must be either 'Member', 'Admin', 'Owner' or 'Client'.");
    }
}