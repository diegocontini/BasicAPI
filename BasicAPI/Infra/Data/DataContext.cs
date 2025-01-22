using BasicAPI.Models.Entities;
using BasicAPI.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Features.Infra.Data
{
    public class DataContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        public void Authenticate(AuthParameters Auth)
        {
            {
                var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder
                {
                    Host = "basicdb",
                    Database = "basicapi",
                    Username = Auth.UserName,
                    Password = Auth.Password
                };

                var newConnectionString = connectionStringBuilder.ToString();

                Configuration["ConnectionStrings:BasicConnection"] = newConnectionString;
                this.Database.GetDbConnection().ConnectionString = newConnectionString;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Host=basicdb;Username=default;Password=default;Database=basicapi";
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("BasicConnection"));
               optionsBuilder.UseNpgsql(connectionString);
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }



    }
}
