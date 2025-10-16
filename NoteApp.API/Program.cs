using Microsoft.EntityFrameworkCore;
using NoteApp.Logic.Domain.Entity.Note;
using NoteApp.Logic.Domain.Entity.Note.Config;
using NoteApp.Logic.Domain.Entity.Note.Repository.Base;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<NoteEntityConfig>(builder.Configuration.GetSection("Database"));

builder.Services.AddDbContext<NoteDbContext>(opt =>
{
    var cs = builder.Configuration.GetSection("Database:ConnectionString").Value
             ?? "Data Source=note.db";
    opt.UseSqlite(cs);
});

builder.Services.AddScoped<INoteEntityRepository, NoteEntityRepository>();

    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
