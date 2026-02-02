using MediatR;
using Vestis._02_Application.CQRS.StudioMembership.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.StudioMembership.Handlers;

public class CreateStudioMembershipCommandHandler : IRequestHandler<CreateStudioMembershipCommand, StudioMembershipEntity>
{
    private readonly IStudioMembershipRepository _studioMembershipRepository;
    private readonly IUserRepository _userRepository;
    private readonly IStudioRepository _studioRepository;

    public CreateStudioMembershipCommandHandler(
        IStudioMembershipRepository studioMembershipRepository,
        IUserRepository userRepository,
        IStudioRepository studioRepository)
    {
        _studioMembershipRepository = studioMembershipRepository;
        _userRepository = userRepository;
        _studioRepository = studioRepository;
    }

    public async Task<StudioMembershipEntity> Handle(CreateStudioMembershipCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var studio = await _studioRepository.GetByIdAsync(request.StudioId); 
        
        
        var entity = new StudioMembershipEntity(user, request.Role, studio);

        entity = await _studioMembershipRepository.CreateAsync(entity, cancellationToken);

        user.StudioMemberships.Add(entity);
        studio.StudioMemberships.Add(entity);

        return entity;
    }
}
