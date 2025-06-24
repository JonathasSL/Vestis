using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.User.Commands;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password) : IRequest<UserEntity>;
