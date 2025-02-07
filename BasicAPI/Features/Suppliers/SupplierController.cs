using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Features.Supplier;
[ApiController]
[Route("v1/supplier")]
[Produces("application/json")]
public class SupplierController(DataContext dbContext) : Controller
{
    private readonly DataContext _dbContext = dbContext;
    [HttpGet("suppliers")]
    public ActionResult<List<Fornecedor>> GetAll([FromQuery] AuthParameters auth)
    {
        try
        {
            _dbContext.Authenticate(auth);
            List<Funcionario> data = _dbContext.Funcionarios.ToList();
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
