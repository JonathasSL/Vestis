using MediatR;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Address.Query;
using Vestis._02_Application.CQRS.Studio.Query;
using Vestis._02_Application.Models.Studio;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Studio.Handlers;

internal class GetStudiosByIdQueryHandler : IRequestHandler<GetStudioByIdQuery, CommandResult<StudioModel>>
{
    private readonly IStudioRepository _repository;
    private readonly IMediator _mediator;

    public async Task<CommandResult<StudioModel>> Handle(GetStudioByIdQuery request, CancellationToken cancellationToken)
    {
        var studios = await _repository.GetByIdAsync(request.Id);

        if (studios is not null)
        {
            var model = new StudioModel
            {
                Name = studios.Name,
                ContactEmail = studios.ContactEmail,
                PhoneNumber = studios.PhoneNumber,
            };


            if (studios.AddressId.HasValue)
            {
                var addressResult = await _mediator.Send(new GetAddressByIdQuery(studios.AddressId.Value), cancellationToken);

                model.Address = addressResult.Data;
            }

            return CommandResult<StudioModel>.Success(model);
        }
        else
        {
            return CommandResult<StudioModel>.NotFound($"No studios found with the ID: {request.Id}");
        }
    }


    public GetStudiosByIdQueryHandler(IStudioRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
}
