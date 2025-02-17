using Microsoft.AspNetCore.Mvc;
using Vestis._01___Application.Models;
using Vestis.Data;
using Vestis.Entities;
using Vestis.Services;

namespace Vestis.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserModel userModel)
    {
        if (_context.Users.Any(u => u.Email == userModel.Email))
            return BadRequest(new { message = "User with this email already exists" });

        var passwordHash = new PasswordHasher().Hash(userModel.Password);
        var user = new UserEntity(userModel.Name, userModel.Email, passwordHash, userModel.Role);
        
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return Ok(new { token = _jwtService.GenerateToken(user.Id.ToString(), userModel.Email) });
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserModel userModel)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == userModel.Email);
        if (existingUser == null)
            return BadRequest(new { message = "User with this email does not exist" });


        if (!new PasswordHasher().Verify(userModel.Password, existingUser.Password))
            return BadRequest(new { message = "Invalid password" });


        return Ok(new { token = _jwtService.GenerateToken(existingUser.Id.ToString(), userModel.Email) });
    }
}
