using MediatR;
using System.Security.Cryptography;
using System.Text;
using Vestis._02_Application.CQRS.User.Commands;
using Vestis._02_Application.Services.Interfaces.User;

namespace Vestis._02_Application.Services.User;

internal class UserVerificationService : IUserVerificationService
{
    private readonly IMediator _mediator;

    public UserVerificationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public string GenerateVerificationToken()
    {
        var token = RandomNumberGenerator.GetInt32(100_000, 1_000_000);
        return token.ToString("D6");
    }

    public string ComputeSha256(string rawData)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public async Task<string?> VerifyEmailAsync(string email, string code)
    {
        var command = new UserEmailVerificationCommand(email, code);
        return await _mediator.Send(command);
    }
}
