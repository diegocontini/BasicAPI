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

        /// <summary>
        /// Método utilizado para autenticar no banco de dados.
        /// Front deverá passar o usuário e senha do banco para executar a conexão. 
        /// </summary>
        /// <param name="Auth"></param>
        public void Authenticate(AuthParameters Auth)
        {
            {
                //var connectionWithDocker = new Npgsql.NpgsqlConnectionStringBuilder
                //{
                //    Host = "localhost",
                //    Database = "basicapi",
                //    Username = Auth.UserName,
                //    Password = Auth.Password
                //};
                var connectionWithoutDocker = new Npgsql.NpgsqlConnectionStringBuilder
                {
                    Host = "localhost",
                    Port = 5432,
                    Database = Auth.Database,
                    Username = Auth.UserName,
                    Password = Auth.Password
                };

                var newConnectionString = connectionWithoutDocker.ToString();

                Configuration["ConnectionStrings:BasicConnection"] = newConnectionString;
                this.Database.GetDbConnection().ConnectionString = newConnectionString;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            ///Utilizando essa String de conexão só pra criar o objeto. 
            ///Caso não utilize o método [Authenticate] vai dar b.o pq o usuario e a senha não existem no BD.
            ///Comportamento é intencional, pois o front deverá passar o usuário e senha do banco.

            const string defaultConnection = "Host=localhost;Username=default;Password=default;Database=basicdb";
            //old
            //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("BasicConnection"));
            optionsBuilder.UseNpgsql(defaultConnection);
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }



    }
}
