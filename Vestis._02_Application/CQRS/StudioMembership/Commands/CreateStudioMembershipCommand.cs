using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.StudioMembership.Commands;

public record CreateStudioMembershipCommand(
    Guid UserId,
    Guid StudioId,
    string Role = "member") : IRequest<StudioMembershipEntity>;
