using Vestis._02_Application.Common;

namespace Vestis._02_Application.Services.Interfaces.User;

public interface IUserVerificationService
{
    string GenerateVerificationToken();
    string ComputeSha256(string rawData);
    Task<CommandResult<string?>> VerifyEmailAsync(string email, string code);
}
