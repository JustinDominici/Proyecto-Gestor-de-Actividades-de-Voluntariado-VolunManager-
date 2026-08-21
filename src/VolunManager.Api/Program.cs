using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VolunManager.Api.ExceptionHandling;
using VolunManager.Application.Contract;
using VolunManager.Application.Service;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;
using VolunManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Middleware global de excepciones: atrapa cualquier error no controlado
// (por ejemplo, una caida de la base de datos) y devuelve un 500 prolijo
// en vez de un stack trace crudo.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

// Servicios de negocio
builder.Services.AddScoped<IVoluntarioService, VoluntarioService>();
builder.Services.AddScoped<IJornadaService, JornadaService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

// CORS para permitir al cliente Blazor conectarse
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Primero en el pipeline: si algo mas adelante explota, esto lo atrapa.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
