using Microsoft.AspNetCore.Mvc;

namespace Vestis._01_Application.Controllers;

public class UsersController : VestisController 
{
    private readonly ILogger<UsersController> _logger;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { Message = "Lista de negócios retornada com sucesso!" });
    }
}
