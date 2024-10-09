using BasicAPI.Features.Infra.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("BasicConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



if (app.Environment.IsDevelopment())
{

    using (var context = (DataContext)app.Services.GetService<DataContext>())
    {
        if (context != null)
        {
            Console.WriteLine("Iniciado migration");
            context?.Database.Migrate();
            Console.WriteLine("Finalizado migration");
            return;
        }
        Console.WriteLine("DbContext est� nulo");
    }
}
app.Run();
