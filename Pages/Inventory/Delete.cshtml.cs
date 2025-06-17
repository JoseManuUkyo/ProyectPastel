using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.services;

namespace Proyecto_Pastel.Pages.Inventory
{
    public class DeleteModel : PageModel
    {
        private readonly InventarioService _service = new();

        [BindProperty]
        public inventario Ingrediente { get; set; }

        public IActionResult OnGet(int id)
        {
            Ingrediente = _service.ObtenerPorId(id);
            if (Ingrediente == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            _service.EliminarIngrediente(Ingrediente.id_ingrediente);
            return RedirectToPage("./Index");
        }
    }
}
