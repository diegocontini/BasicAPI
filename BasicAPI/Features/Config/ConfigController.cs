using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Features.Config;
public class ConfigController(DataContext dataContext) : Controller
{
    private readonly DataContext _dataContext = dataContext;

    [HttpPost("database/do-pending-migration")]
    public IActionResult Migrate([FromQuery] AuthParameters Auth)
    {
        try
        {
            _dataContext.Authenticate(Auth);
            if (_dataContext.Database.GetPendingMigrations().Any())
            {
                _dataContext.Database.Migrate();
                return Ok("Database migrate sucessfully");
            }
            return Ok("Theres no pending migration");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("database/create-default-registers")]
    public IActionResult CreateDefatulRegisterrs([FromQuery] AuthParameters Auth)
    {
        try
        {
            _dataContext.Authenticate(Auth);
            CreateProducts();
            CreateSuppliers();
            CreateEmployees();
            return Ok("Default registers created sucessfully!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    private void CreateProducts()
    {
        int qtd = _dataContext.Produtos.Count();

        if (qtd > 0) 
        { 
            return;
        }
        var produtos = new List<Produto>
        {
            new Produto { Codigo = 1, Descricao = "Leite Integral", Valor = 3.50m, Quantidade = 10.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 2, Descricao = "Leite Desnatado", Valor = 3.00m, Quantidade = 15.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 3, Descricao = "Iogurte Natural", Valor = 2.00m, Quantidade = 20.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 4, Descricao = "Queijo Minas Frescal", Valor = 8.00m, Quantidade = 5.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 5, Descricao = "Manteiga", Valor = 4.50m, Quantidade = 25.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 6, Descricao = "Creme de Leite", Valor = 3.75m, Quantidade = 30.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 7, Descricao = "Requeijão", Valor = 5.00m, Quantidade = 12.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 8, Descricao = "Leite Condensado", Valor = 6.00m, Quantidade = 8.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 9, Descricao = "Queijo Gouda", Valor = 10.00m, Quantidade = 7.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 10, Descricao = "Leite em Pó", Valor = 2.50m, Quantidade = 18.0m, FornecedorCodigo = 1 },
        };
        _dataContext.Produtos.AddRange(produtos);
        _dataContext.SaveChanges();
    }

    private void CreateSuppliers()
    {
        int qtd = _dataContext.Fornecedores.Count();
        if (qtd > 0) 
        {
            return;
        }
        var suppliers = new List<Fornecedor>
        {
            new Fornecedor { Codigo= 1, Descricao="Frimesa"},
            new Fornecedor { Codigo= 2, Descricao="Italate"},
        };
        _dataContext.Fornecedores.AddRange(suppliers);
        _dataContext.SaveChanges();
    }

    private void CreateEmployees()
    {
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Codigo = 1, Nome = "João Silva", CPF = "123.456.789-00", Senha = "senha123", Funcao = "Gerente" },
            new Funcionario { Codigo = 2, Nome = "Maria Oliveira", CPF = "987.654.321-00", Senha = "senha456", Funcao = "Assistente Administrativo" },
            new Funcionario { Codigo = 3, Nome = "Carlos Santos", CPF = "456.123.789-00", Senha = "senha789", Funcao = "Analista de TI" },
            new Funcionario { Codigo = 4, Nome = "Ana Costa", CPF = "321.654.987-00", Senha = "senha012", Funcao = "Contador" },
            new Funcionario { Codigo = 5, Nome = "Bruno Lima", CPF = "789.123.456-00", Senha = "senha345", Funcao = "Vendedor" },
        };
        _dataContext.Funcionarios.AddRange(funcionarios);   
        _dataContext.SaveChanges();
    }
        
}
