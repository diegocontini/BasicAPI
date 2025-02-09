using BasicAPI.Config;
using BasicAPI.Features.Infra.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel((context, opt) => {
//    opt.ListenAnyIP(8081);
//});
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
                                            options.UseNpgsql(builder.Configuration.GetConnectionString("BasicConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    //serverOptions.Listen(IPAddress.Parse(ip), 18002);
    serverOptions.ListenAnyIP(18002);
    
}).UseUrls("http://localhost:18002");
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI((c) =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Trabalho de banco de dados II");
    c.RoutePrefix= string.Empty;
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();




app.Run();
