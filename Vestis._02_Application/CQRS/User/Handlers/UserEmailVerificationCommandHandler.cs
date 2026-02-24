using MediatR;
using System.Linq;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Services;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

internal class UserEmailVerificationCommandHandler : IRequestHandler<UserEmailVerificationCommand, string?>
{
    private readonly IEmailVerificationTokenEntityRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly BusinessNotificationContext _businessNotificationContext;

    public UserEmailVerificationCommandHandler(
        IEmailVerificationTokenEntityRepository tokenRepository,
        IUserRepository userRepository,
        JwtService jwtService,
        BusinessNotificationContext businessNotificationContext)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _businessNotificationContext = businessNotificationContext;
    }

    public async Task<string?> Handle(UserEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var tokens = await _tokenRepository.GetValidByEmailAndCodeAsync(request.Email, request.Code, cancellationToken);
        var token = tokens.FirstOrDefault();

        if (token == null)
        {
            _businessNotificationContext.Add("Token não encontrado.");
            return null;
        }

        if (token.IsExpired())
        {
            _businessNotificationContext.Add("Token expirado. Solicite um novo. Duração do token: 30 minutos.");
            return null;
        }

        if (token.IsUsed)
        {
            _businessNotificationContext.Add("Token não encontrado.");
            return null;
        }

        var user = token.User;

        token.Use();
        user.ConfirmEmail();

        await _tokenRepository.Update(token);
        await _userRepository.Update(user);

        return _jwtService.GenerateToken(user.Id.ToString(), user.Email);
    }
}
