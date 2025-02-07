using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Features.Users;


[ApiController]
[Route("v1/user")]
[Produces("application/json")]
public class UserController(DataContext dbContext) : Controller
{
    private readonly DataContext _dbContext = dbContext;
    [HttpPost("login")]
    public IActionResult Login([FromQuery] AuthParameters authParam)
    {
        try
        {
            _dbContext.Authenticate(authParam);

            return Ok("Logado com sucesso");
        }
        catch (Exception e)
        {
            return Unauthorized(new ErrorResponse { Error = e.Message });
        }
    }
}
