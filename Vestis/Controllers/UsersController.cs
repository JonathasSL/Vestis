using Microsoft.AspNetCore.Mvc;

namespace Controllers;

public class UsersController : VestisController 
{
    private readonly ILogger<UsersController> _logger;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { Message = "Lista de negócios retornada com sucesso!" });
    }
}
