using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._04_Infrasctructure.Data;

namespace Vestis._01_Presentation.Controllers;

public class AuthController : VestisController
{
    private readonly IUserService _userService;

    public AuthController(ApplicationDbContext context, IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost()]
    public async Task<IActionResult> RegisterAsync([FromBody] UserModel userModel)
    {
        if (await _userService.ExistsAsync(userModel.Email))
            return BadRequest(new { message = "User with this email already exists" });
        
        var user = await _userService.Create(userModel);
        //TODO: Send Email validation instead of authenticate
        //var token = await _userService.AuthenticateAsync(user.Email, userModel.Password);
        
        if (user != null)
            return Created();
        else
            return BadRequest();
    }
    
    [HttpPost()]
    public async Task<IActionResult> Login([FromBody] UserModel userModel)
    {
        if (userModel == null || string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
            return BadRequest(new { message = "Email and password are required" });

        var token = await _userService.AuthenticateAsync(userModel.Email, userModel.Password);

        return token != null ? Ok(new { token }) : Unauthorized();
    }
}
