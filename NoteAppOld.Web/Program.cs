using NoteApp.Logic.Domain.Entity.Note;
using NoteApp.Logic.Domain.Entity.Note.Repository.Base;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;
using NoteApp.Logic.Domain.Entity.Note.Service;
using NoteApp.Logic.Domain.Entity.Note.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<INoteEntityRepository, NoteEntityRepository>();
builder.Services.AddDbContext<NoteDbContext>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddHttpClient("NoteAppAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

// app.MapRazorComponents<App>()
//     .AddInteractiveServerRenderMode();

app.Run();
