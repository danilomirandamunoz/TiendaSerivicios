using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteServices;
using TiendaServicios.CarritoCompra.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IAutorService, AutorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoCarrito>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnection"));

});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

builder.Services.AddHttpClient("Autores", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autores"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
