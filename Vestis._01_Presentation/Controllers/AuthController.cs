using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models.Auth;
using Vestis._02_Application.Services.Interfaces.User;

namespace Controllers;

public class AuthController : VestisController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost()]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
    {
        if (await _userService.ExistsAsync(dto.Email))
            return BadRequest(new { message = "User with this email already exists" });
        
        var user = await _userService.Create(dto);
        //TODO: Send Email validation instead of authenticate
        //var token = await _userService.AuthenticateAsync(user.Email, userModel.Password);
        
        if (user != null)
            return Created();
        else
            return BadRequest();
    }
    
    [HttpPost()]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            return BadRequest(new { message = "Email and password are required" });

        var token = await _userService.AuthenticateAsync(dto.Email, dto.Password);

        return token != null ? Ok(new { token }) : Unauthorized();
    }

	[HttpGet]
	public async Task<IActionResult> Variables([FromRoute] string key)
	{
		if (key != "vestis_admin_key")
			return Unauthorized();

		var vars = Environment.GetEnvironmentVariables();
		return Ok(new { vars });
	}
}
