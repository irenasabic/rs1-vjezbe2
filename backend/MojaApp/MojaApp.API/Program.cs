using Microsoft.AspNetCore.Mvc.ModelBinding;
using MojaApp.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

StudentStorage.Studenti.Add(new () { Id = 1, Ime="A", Prezime="B"});
StudentStorage.Studenti.Add(new() { Id = 2, Ime = "C", Prezime = "D" });

app.Run();
