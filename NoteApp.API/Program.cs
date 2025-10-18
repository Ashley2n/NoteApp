using Microsoft.EntityFrameworkCore;
using NoteApp.Logic.Domain.Entity.Note;
using NoteApp.Logic.Domain.Entity.Note.Config;
using NoteApp.Logic.Domain.Entity.Note.Repository.Base;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;
using NoteApp.Logic.Domain.Entity.Note.Service;
using NoteApp.Logic.Domain.Entity.Note.Service.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<NoteEntityConfig>(builder.Configuration.GetSection("Database"));

builder.Services.AddDbContext<NoteDbContext>(opt =>
{
    var cs = builder.Configuration.GetSection("Database:ConnectionString").Value
             ?? "Data Source=../NoteApp.Logic/note.db";
    opt.UseSqlite(cs);
});

builder.Services.AddScoped<INoteEntityRepository, NoteEntityRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


    
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opts.RoutePrefix = string.Empty;
    });
}
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


// curl -X POST https://localhost:5269/api/Note/1 \ -H 'Content-Type: application/json' \ -d '{"id": 0,"title": "t1","content": "c1","created": "2025-10-16T15:58:37.241Z" }'

// curl -X 'GET' \'http://localhost:5269/api/Note' \ -H 'accept: */*'
// curl -i http://localhost:5269/api/Note