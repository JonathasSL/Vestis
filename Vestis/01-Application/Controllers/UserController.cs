using Microsoft.AspNetCore.Mvc;

namespace Vestis._01_Application.Controllers;

public class UserController : BaseController 
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { Message = "Lista de negócios retornada com sucesso!" });
    }
}
