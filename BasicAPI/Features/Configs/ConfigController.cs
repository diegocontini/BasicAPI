using BasicAPI.Features.Configs;
using BasicAPI.Features.Infra.Data;
using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Diagnostics;
using System.Net;

namespace BasicAPI.Features.Config;

[ApiController]
[Route("v1/configuration")]
[Produces("application/json")]
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
            return StatusCode(500, new ErrorResponse { Error = ex.Message });
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
            return StatusCode(500, new ErrorResponse { Error = ex.Message });
        }
    }

    [HttpPost("database/create-backup")]
    public async Task<IActionResult> CreateBackup([FromQuery] AuthParameters Auth)
    {
        ConfigService service = new ConfigService ();
        try
        {
            _dataContext.Authenticate(Auth);
            string conn = _dataContext.Database.GetDbConnection().ConnectionString;
            



            await service.PostgreSqlDump(
                outFile: Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\backup_postgres",
                host:  "localhost",
                port: "5432",
                database: Auth.Database,
                user: Auth.UserName,
                password: Auth.Password
            );
            return Ok("backup created sucessfully");
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse { Error = e.Message });

        }
    }


    [HttpPost("database/restore-last-backup")]
    public async Task<IActionResult> RestoreLastBackup([FromQuery] AuthParameters Auth)
    {
        ConfigService service = new ConfigService();
        try
        {
            _dataContext.Authenticate(Auth);
            string conn = _dataContext.Database.GetDbConnection().ConnectionString;




            await service.PostgreSqlRestore(
                inputFile: Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\backup_postgres",
                host: "localhost",
                port: "5432",
                database: Auth.Database,
                user: Auth.UserName,
                password: Auth.Password
            );
            return Ok("backup restored sucessfully");
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse { Error = e.Message });

        }
    }


    [HttpGet("database/users")]
    public async Task<IActionResult> GetAllDatabaseUsers([FromQuery] AuthParameters Auth)
    {
        try
        {
            _dataContext.Authenticate(Auth);
            var data =  _dataContext.Database.ExecuteSqlRaw("select rolname from pg_catalog.pg_roles where rolcanlogin = true;");

                return Ok(data);
        } catch (Exception e)
        {
            return StatusCode(500, new ErrorResponse { Error = e.Message + e.InnerException });
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
            new Produto { Codigo = 1, Descricao = "Filtro de Óleo", Valor = 3.50m, Quantidade = 10.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 2, Descricao = "Filtro de Ar", Valor = 3.00m, Quantidade = 15.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 3, Descricao = "Pastilha de Freio", Valor = 2.00m, Quantidade = 20.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 4, Descricao = "Bateria Automotiva", Valor = 8.00m, Quantidade = 5.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 5, Descricao = "Pneu 175/65 R14", Valor = 4.50m, Quantidade = 25.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 6, Descricao = "Óleo de Motor 5W30", Valor = 3.75m, Quantidade = 30.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 7, Descricao = "Velas de Ignição", Valor = 5.00m, Quantidade = 12.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 8, Descricao = "Correia Dentada", Valor = 6.00m, Quantidade = 8.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 9, Descricao = "Amortecedor Dianteiro", Valor = 10.00m, Quantidade = 7.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 10, Descricao = "Kit de Embreagem", Valor = 2.50m, Quantidade = 18.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 11, Descricao = "Radiador", Valor = 15.00m, Quantidade = 5.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 12, Descricao = "Bomba de Combustível", Valor = 12.00m, Quantidade = 7.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 13, Descricao = "Alternador", Valor = 20.00m, Quantidade = 4.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 14, Descricao = "Motor de Partida", Valor = 18.00m, Quantidade = 6.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 15, Descricao = "Disco de Freio", Valor = 7.00m, Quantidade = 10.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 16, Descricao = "Cabo de Velas", Valor = 3.00m, Quantidade = 15.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 17, Descricao = "Sensor de Oxigênio", Valor = 5.00m, Quantidade = 8.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 18, Descricao = "Catalisador", Valor = 25.00m, Quantidade = 3.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 19, Descricao = "Eixo de Transmissão", Valor = 30.00m, Quantidade = 2.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 20, Descricao = "Filtro de Combustível", Valor = 2.50m, Quantidade = 20.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 21, Descricao = "Filtro de Cabine", Valor = 3.00m, Quantidade = 18.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 22, Descricao = "Correia Poly V", Valor = 4.00m, Quantidade = 12.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 23, Descricao = "Sensor de Temperatura", Valor = 6.00m, Quantidade = 10.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 24, Descricao = "Sensor de Pressão", Valor = 7.50m, Quantidade = 9.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 25, Descricao = "Vela de Aquecimento", Valor = 5.50m, Quantidade = 14.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 26, Descricao = "Bomba de Água", Valor = 10.00m, Quantidade = 6.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 27, Descricao = "Cilindro Mestre", Valor = 8.00m, Quantidade = 7.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 28, Descricao = "Cilindro de Roda", Valor = 6.50m, Quantidade = 8.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 29, Descricao = "Cilindro de Embreagem", Valor = 9.00m, Quantidade = 5.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 30, Descricao = "Coxim do Motor", Valor = 4.50m, Quantidade = 11.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 31, Descricao = "Coxim do Cambio", Valor = 5.00m, Quantidade = 10.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 32, Descricao = "Coxim do Amortecedor", Valor = 3.50m, Quantidade = 12.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 33, Descricao = "Coxim do Escapamento", Valor = 2.00m, Quantidade = 15.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 34, Descricao = "Coxim do Radiador", Valor = 1.50m, Quantidade = 20.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 35, Descricao = "Coxim do Diferencial", Valor = 7.00m, Quantidade = 8.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 36, Descricao = "Coxim do Cardan", Valor = 6.00m, Quantidade = 9.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 37, Descricao = "Coxim do Motor Traseiro", Valor = 8.50m, Quantidade = 7.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 38, Descricao = "Coxim do Motor Dianteiro", Valor = 9.50m, Quantidade = 6.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 39, Descricao = "Coxim do Cambio Traseiro", Valor = 10.00m, Quantidade = 5.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 40, Descricao = "Coxim do Cambio Dianteiro", Valor = 11.00m, Quantidade = 4.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 41, Descricao = "Coxim do Amortecedor Traseiro", Valor = 12.00m, Quantidade = 3.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 42, Descricao = "Coxim do Amortecedor Dianteiro", Valor = 13.00m, Quantidade = 2.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 43, Descricao = "Coxim do Escapamento Traseiro", Valor = 14.00m, Quantidade = 1.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 44, Descricao = "Coxim do Escapamento Dianteiro", Valor = 15.00m, Quantidade = 1.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 45, Descricao = "Coxim do Radiador Traseiro", Valor = 16.00m, Quantidade = 1.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 46, Descricao = "Coxim do Radiador Dianteiro", Valor = 17.00m, Quantidade = 1.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 47, Descricao = "Coxim do Diferencial Traseiro", Valor = 18.00m, Quantidade = 1.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 48, Descricao = "Coxim do Diferencial Dianteiro", Valor = 19.00m, Quantidade = 1.0m, FornecedorCodigo = 2 },
            new Produto { Codigo = 49, Descricao = "Coxim do Cardan Traseiro", Valor = 20.00m, Quantidade = 1.0m, FornecedorCodigo = 1 },
            new Produto { Codigo = 50, Descricao = "Coxim do Cardan Dianteiro", Valor = 21.00m, Quantidade = 1.0m, FornecedorCodigo = 2 }
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
            new Fornecedor { Codigo= 1, Descricao="Bosch"},
            new Fornecedor { Codigo= 2, Descricao="NGK"},
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
