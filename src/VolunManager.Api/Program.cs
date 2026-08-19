using Microsoft.EntityFrameworkCore;
using VolunManager.Application.Contract;
using VolunManager.Application.Service;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;
using VolunManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VolunManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VolunManagerConnection")));

// Repositorios
builder.Services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
builder.Services.AddScoped<IJornadaRepository, JornadaRepository>();

// Servicios de negocio
builder.Services.AddScoped<IVoluntarioService, VoluntarioService>();
builder.Services.AddScoped<IJornadaService, JornadaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();