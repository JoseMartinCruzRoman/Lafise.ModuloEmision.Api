
using Microsoft.EntityFrameworkCore;
using Lafise.ModuloEmision.Api.Data;
using Lafise.ModuloEmision.Api.Data.Repositories;
using Lafise.ModuloEmision.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPolizaRepository, PolizaRepository>();
builder.Services.AddScoped<EmisionService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Auto creacion de la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Lafise.ModuloEmision.Api.Data.ApplicationDbContext>();
        context.Database.EnsureCreated();

        if (!context.Coberturas.Any())
        {
            context.Coberturas.AddRange(
                new Lafise.ModuloEmision.Api.Models.Entities.Cobertura { Nombre = "Choque / Daños Materiales", Tasa = 2.50m },
                new Lafise.ModuloEmision.Api.Models.Entities.Cobertura { Nombre = "Robo Total", Tasa = 1.80m },
                new Lafise.ModuloEmision.Api.Models.Entities.Cobertura { Nombre = "Responsabilidad Civil (Daños a Terceros)", Tasa = 1.20m }
            );
            context.SaveChanges();
        }

        if (!context.Clientes.Any())
        {
            context.Clientes.AddRange(
                new Lafise.ModuloEmision.Api.Models.Entities.Cliente { Identificacion = "001-150595-0002A", PrimerNombre = "Jose", SegundoNombre = "Martin", PrimerApellido = "Cruz", SegundoApellido = "Roman", Correo = "jose.cruz@gmail.com" },
                new Lafise.ModuloEmision.Api.Models.Entities.Cliente { Identificacion = "401-201088-0001B", PrimerNombre = "Maria", PrimerApellido = "Perez", Correo = "maria.lopez@gmail.com" }
            );
            context.SaveChanges();
        }

        Console.WriteLine("Base de datos creada o ya existe.");
    }
    catch(Exception ex)
    { 
        Console.WriteLine($"error al inicializar la base de datos: {ex.Message}");
    }
}

app.Run();
