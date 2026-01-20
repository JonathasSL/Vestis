using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Studio;

namespace Vestis._02_Application.CQRS.Studio.Query;

internal record GetStudiosByUserIdQuery(Guid UserId) : IRequest<CommandResult<List<StudioSummaryModel>>>;
