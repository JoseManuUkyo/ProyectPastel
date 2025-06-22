using Microsoft.EntityFrameworkCore;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.DAOs; // Para UsuarioDAO
using Proyecto_Pastel.services; // Para IProduccionService y ProduccionService
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto con Pomelo y versión específica de MySQL
builder.Services.AddDbContext<proyecto_pastelContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

// Registro de servicios adicionales
builder.Services.AddRazorPages();
builder.Services.AddSession();

// Registro de DAOs y servicios personalizados
builder.Services.AddScoped<UsuarioDAO>();
builder.Services.AddScoped<IProduccionService, ProduccionService>();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();