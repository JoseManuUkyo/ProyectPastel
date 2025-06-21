using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Pastel.Entities;
using System;
using System.Linq;

namespace Proyecto_Pastel.Pages.Inventory
{
    public class RegistrarSalidaModel : PageModel
    {
        private readonly proyecto_pastelContext _context;

        public RegistrarSalidaModel(proyecto_pastelContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IdIngrediente { get; set; }

        [BindProperty]
        public decimal Cantidad { get; set; }

        [BindProperty]
        public string Motivo { get; set; } = string.Empty;

        [BindProperty]
        public string? Descripcion { get; set; }

        public List<SelectListItem> Ingredientes { get; set; } = new();

        public string? MensajeExito { get; set; }

        public List<movimientos_inventario> SalidasRegistradas { get; set; } = new List<movimientos_inventario>();


        public void OnGet()
        {
            SalidasRegistradas = _context.movimientos_inventario
            .Where(m => m.tipo_movimiento == "salida")
            .Include(m => m.id_ingredienteNavigation)
            .Include(m => m.id_usuarioNavigation)
            .OrderByDescending(m => m.fecha_movimiento)
            .ToList();

            Ingredientes = _context.inventario
                .Select(i => new SelectListItem
                {
                    Value = i.id_ingrediente.ToString(),
                    Text = i.nombre
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet(); // Recargar lista de ingredientes
                return Page();
            }

            var ingrediente = _context.inventario.FirstOrDefault(i => i.id_ingrediente == IdIngrediente);
            if (ingrediente == null)
            {
                ModelState.AddModelError(string.Empty, "Ingrediente no encontrado.");
                OnGet();
                return Page();
            }

            if (Cantidad <= 0)
            {
                ModelState.AddModelError(string.Empty, "La cantidad debe ser mayor a 0.");
                OnGet();
                return Page();
            }

            if (ingrediente.cantidad < Cantidad)
            {
                ModelState.AddModelError(string.Empty, "No hay suficiente cantidad en inventario.");
                OnGet();
                return Page();
            }

            var nuevoMovimiento = new movimientos_inventario
            {
                id_ingrediente = IdIngrediente,
                id_usuario = 1, // ← Cambiar esto si tienes autenticación
                tipo_movimiento = "salida",
                motivo = Motivo,
                cantidad = Cantidad,
                descripcion = Descripcion
            };

            ingrediente.cantidad -= Cantidad;

            _context.movimientos_inventario.Add(nuevoMovimiento);
            _context.SaveChanges();

            TempData["MensajeExito"] = "✅ Salida registrada correctamente.";
            return RedirectToPage("/Inventory/RegistrarSalida");
        }
    }
}
