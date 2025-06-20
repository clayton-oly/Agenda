using Npgsql;
using System.Data;
using Agenda.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DbConexaoFactory>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();