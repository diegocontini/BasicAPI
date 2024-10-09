using BasicAPI.Features.Auth;
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
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("BasicConnection"));
        }

        public DbSet<License> Licenses { get; set; }
    }
}
