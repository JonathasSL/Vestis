using MediatR;
using Vestis._02_Application.Studio.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Studio.Handlers;

public class CreateStudioCommandHandler : IRequestHandler<CreateStudioCommand, StudioEntity>
{
    private readonly IStudioRepository _studioRepository;
    //private readonly IUnitOfWork _unitOfWork;
    public CreateStudioCommandHandler(IStudioRepository studioRepository/*, IUnitOfWork unitOfWork*/)
    {
        _studioRepository = studioRepository;
        //_unitOfWork = unitOfWork;
    }
    public async Task<StudioEntity> Handle(CreateStudioCommand request, CancellationToken cancellationToken)
    {
        var studio = new StudioEntity(request.Name);
        studio.ChangeContactEmail(request.ContactEmail);
        studio.ChangePhoneNumber(request.PhoneNumber);

        studio = await _studioRepository.CreateAsync(studio);
        //await _unitOfWork.SaveChangesAsync();
        return studio;
    }
}