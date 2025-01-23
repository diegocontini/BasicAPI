using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Features.Products;

[ApiController]
[Route("v1/product")]
[Produces("application/json")]
public class ProductController(DataContext dbContext) : Controller
{
    private readonly DataContext _dbContext = dbContext;
    [HttpGet("products")]
    public ActionResult<List<Produto>> GetAll([FromQuery] AuthParameters auth)
    {
        try
        {
            _dbContext.Authenticate(auth);
            var data = _dbContext.Produtos.ToList();
            if (data.Count == 0)
            {
                return NoContent();
            }
            return Ok(data);
        }
        catch (Exception ex) {
            return StatusCode(500, new ErrorResponse { Error = ex.Message });
        }
    }
}
