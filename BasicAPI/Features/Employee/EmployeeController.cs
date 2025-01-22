using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Features.Employee;
public class EmployeeController(DataContext dbContext) : Controller
{
    private readonly DataContext _dbContext = dbContext;
    [HttpGet("employees")]
    public ActionResult<List<Funcionario>> GetAll([FromQuery] AuthParameters auth)
    {
        try
        {
            _dbContext.Authenticate(auth);
            var data = _dbContext.Funcionarios.ToList();
            if (data.Count == 0)
            {
                return NoContent();
            }
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse { Error = ex.Message });
        }
    }
}
