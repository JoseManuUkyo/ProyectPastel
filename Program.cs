using Microsoft.EntityFrameworkCore;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.services;

var builder = WebApplication.CreateBuilder(args);

//Registro de salidas
builder.Services.AddDbContext<proyecto_pastelContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

// Registro del servicio de producción
builder.Services.AddScoped<IProduccionService, ProduccionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HTTPS Strict Transport Security
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
