using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.services;

namespace Proyecto_Pastel.Pages.Inventory
{
    public class EditModel : PageModel
    {
        private readonly InventarioService _service = new();

        [BindProperty]
        public inventario Ingrediente { get; set; }

        public IActionResult OnGet(int id)
        {
            var ingrediente = _service.ObtenerPorId(id);
            if (ingrediente == null)
                return NotFound();

            Ingrediente = ingrediente;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _service.ActualizarIngrediente(Ingrediente);
            return RedirectToPage("./Index");
        }
    }
}