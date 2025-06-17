using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.services;

namespace Proyecto_Pastel.Pages.Inventory;

public class CreateModel : PageModel
{
    private readonly InventarioService _service;

    public CreateModel()
    {
        _service = new InventarioService();
    }

    [BindProperty]
    public inventario Ingrediente { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        _service.AgregarIngrediente(Ingrediente);
        return RedirectToPage("Index");
    }
}
