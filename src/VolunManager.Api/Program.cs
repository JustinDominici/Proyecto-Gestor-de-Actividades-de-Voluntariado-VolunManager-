using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VolunManager.Application.Contract;
using VolunManager.Application.Service;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;
using VolunManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Los enums (como EstadoTarea) se envian y reciben como texto
        // ("Pendiente") en vez de numero (0), para que sea legible en Swagger.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<VolunManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VolunManagerConnection")));

// Repositorios
builder.Services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
builder.Services.AddScoped<IJornadaRepository, JornadaRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();

// Servicios de negocio
builder.Services.AddScoped<IVoluntarioService, VoluntarioService>();
builder.Services.AddScoped<IJornadaService, JornadaService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITareaService, TareaService>();

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