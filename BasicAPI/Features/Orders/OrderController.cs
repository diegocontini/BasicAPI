using AutoMapper;
using BasicAPI.Features.Infra.Data;
using BasicAPI.Features.Orders.DTOs;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BasicAPI.Features.Orders;



[ApiController]
[Route("v1/order")]
[Produces("application/json")]
public class OrderController(DataContext dbContext, IMapper mapper) : Controller
{
    private readonly DataContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    [HttpPost("create-order")]
    public async Task<ActionResult<VendaPostResponseDTO>> CreateOrder([FromQuery] AuthParameters authParam, [FromBody] VendaPostDTO order)
    {

        _dbContext.Authenticate(authParam);
        await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var venda = _mapper.Map<Venda>(order);
            var itens = _mapper.Map<ICollection<VendaItem>>(order.Itens);

            await _dbContext.Vendas.AddAsync(venda);
            await _dbContext.SaveChangesAsync();


            ///Adicionar a venda, para depois validar se tem itens, de primeiro momento parece errado. E de fato é xD.
            ///Porém, feito desta forma para executar o rollback posteriormente.
           if (!order.Itens.Any())
            {
                await _dbContext.Database.RollbackTransactionAsync();
                return BadRequest(new ErrorResponse { Error = "Um pedido não pode ser enviado sem itens" });
            } 
            foreach (var item in itens)
            {
                item.VendaCodigo = venda.Codigo;




                ///Utilizado a estratégia de Pessimistic Locking.
                ///Com o FOR UPDATE, a tabela será "travada" até que este recurso seja liberado.
                ///Outras transações concorrentes, aguardarão a tabela ficar disponível para processar a query
                var estoque = await _dbContext.Produtos
                    .FromSql($"select \"pro_quantidade\" from \"tb_produtos\" where \"pro_codigo\" = {item.ProdutoCodigo} for update")
                    .Select(p => p.Quantidade)
                    .FirstOrDefaultAsync();



                var produto = await _dbContext.Produtos.Where(p => p.Codigo == item.ProdutoCodigo).SingleOrDefaultAsync();

                if (produto == null || produto == default)
                {
                    await _dbContext.Database.RollbackTransactionAsync();
                    return BadRequest(new ErrorResponse { Error = $"Produto de código ${item.ProdutoCodigo} não encontrado" });
                }


                if ((estoque - item.Quantidade) < 0)
                {
                    await _dbContext.Database.RollbackTransactionAsync();
                    return BadRequest(new ErrorResponse { Error = $"Produto de código sem estoque disponível" });
                }

                ///Se tudo estiver ok, deduz a quantidade comprada do estoque.
                produto.Quantidade = estoque - item.Quantidade;
                await _dbContext.SaveChangesAsync();


            }

            await _dbContext.VendaItens.AddRangeAsync(itens);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Database.CommitTransactionAsync();


            return Ok();

        }
        catch (Exception ex)
        {
            await _dbContext.Database.RollbackTransactionAsync();
            return StatusCode(500, new ErrorResponse { Error = ex.Message + ex.InnerException });
        }

    }


    [HttpGet("orders")]
    public async Task<ActionResult<Venda>> GetAll([FromQuery] AuthParameters authParam) {
        _dbContext.Authenticate(authParam);

        try
        {
            var qryRes = await _dbContext.Vendas.ToListAsync();
            return Ok(qryRes);
        }
        catch (Exception ex) {
            return StatusCode(500, new ErrorResponse { Error = ex.Message });
        
        }
        
    }
}
