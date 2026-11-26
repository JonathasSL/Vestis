using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Client;

namespace Vestis._02_Application.CQRS.Client.Query;

internal record GetClientByIdQuery(Guid ClientId, Guid StudioId): IRequest<CommandResult<ClientModel>>;