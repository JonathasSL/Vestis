using MediatR;
using Vestis._02_Application.CQRS.Studio.Commands;
using Vestis._02_Application.CQRS.StudioMembership.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Studio.Handlers;

public class CreateStudioCommandHandler : IRequestHandler<CreateStudioCommand, StudioEntity>
{
    private readonly IStudioRepository _studioRepository;
    private IMediator _mediator;

    public CreateStudioCommandHandler(IStudioRepository studioRepository, IMediator mediator)
    {
        _studioRepository = studioRepository;
        _mediator = mediator;
    }

    public async Task<StudioEntity> Handle(CreateStudioCommand request, CancellationToken cancellationToken)
    {

        var studio = new StudioEntity(request.Name);
        studio.ChangeContactEmail(request.ContactEmail);
        studio.ChangePhoneNumber(request.PhoneNumber);

        if (request.AddressCommand is not null)
        {
            var address = await _mediator.Send(request.AddressCommand, cancellationToken);
            studio.ChangeAddress(address);
        }

        studio = await _studioRepository.CreateAsync(studio, cancellationToken);

       _ = await _mediator.Send(new CreateStudioMembershipCommand(request.UserId, studio.Id, "Owner"), cancellationToken);

        return studio;
    }
}