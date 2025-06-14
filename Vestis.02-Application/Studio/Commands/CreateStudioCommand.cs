using MediatR;

namespace Vestis._02_Application.Studio.Commands;

public record CreateStudioCommand(string Name, string ContactEmail, string PhoneNumber) : IRequest<Guid>;