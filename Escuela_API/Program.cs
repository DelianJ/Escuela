using Escuela_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbescuelaContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("coneccionSQL"));
}
);

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("nueva politica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("nueva politica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
