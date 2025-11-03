using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vestis._02_Application.Services.Interfaces;
using Vestis.Shared.Extensions;

namespace Controllers;

[Authorize]
public class UsersController : VestisController 
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Me()
    {
        var userId = User.GetUserId();

        var user = await _userService.GetById(userId.Value);

        return Ok(user);
    }
}
