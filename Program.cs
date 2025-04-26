using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Context;
using ProjetoClinica.Repositories;
using ProjetoClinica.Services;
using ProjetoClinica.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
