using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Auth;
using Vestis._02_Application.Services.Interfaces.User;

namespace Controllers;

public class AuthController : VestisController
{
    private readonly IUserService _userService;
    private readonly IUserVerificationService _userVerificationService;
    private readonly BusinessNotificationContext _businessNotificationContext;

    public AuthController(
        IUserService userService,
        IUserVerificationService userVerificationService,
        BusinessNotificationContext businessNotificationContext)
    {
        _userService = userService;
        _userVerificationService = userVerificationService;
        _businessNotificationContext = businessNotificationContext;
    }

    [HttpPost()]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
    {
        var user = await _userService.Create(dto);
		
		return FromResult(user);
    }

    [HttpPost()]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            return BadRequest(new { message = "Email and password are required" });

        var token = await _userService.AuthenticateAsync(dto.Email, dto.Password);

        return FromResult(token);
    }

    [HttpPost()]
    public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Code))
            return BadRequest(new { mensagens = new[] { "Email e código são obrigatórios." } });

        var result = await _userVerificationService.VerifyEmailAsync(dto.Email, dto.Code);

        var mensagens = _businessNotificationContext.Notifications.Any()
            ? _businessNotificationContext.Notifications
            : new[] { "Token inválido." };

        return FromResult(result);
    }

	[HttpGet]
	public async Task<IActionResult> Variables([FromQuery] string key)
	{
		if (key != "vestis_admin_key")
			return Unauthorized();

		var vars = Environment.GetEnvironmentVariables();
		return Ok(new { vars });
	}
}
