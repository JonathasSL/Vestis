using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Studio.Query;
using Vestis._02_Application.Models.Studio;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Studio.Handlers;

internal class GetStudiosByUserIdQueryHandler : IRequestHandler<GetStudiosByUserIdQuery, CommandResult<List<StudioSummaryModel>>>
{
    private readonly IStudioRepository _repository;

    public GetStudiosByUserIdQueryHandler(IStudioRepository repository)
    {
        _repository = repository;
    }

    public Task<CommandResult<List<StudioSummaryModel>>> Handle(GetStudiosByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var studios = _repository.GetByUserAsyncReadOnly(request.UserId, cancellationToken).Result;

            var models = studios.Select(s => new StudioSummaryModel
            {
                Id = s.Id,
                Name = s.Name,
                RoleInStudio = s.StudioMemberships.FirstOrDefault(m => m.UserId.Equals(request.UserId))?.Role,
            }).OrderBy(s => s.Name).ToList();

            return Task.FromResult(CommandResult<List<StudioSummaryModel>>.Success(models));
        }
        catch (Exception e)
        {
            return Task.FromResult(CommandResult<List<StudioSummaryModel>>.Failure(e.Message));
        }
    }
}
