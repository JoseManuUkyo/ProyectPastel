using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.Pages
{
   public class AgregarUsuarioModel : PageModel
{
    [BindProperty]
    public usuarios usuarios { get; set; }

    [BindProperty]
    public IFormFile ImagenArchivo { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        // Aquí iría el guardado del usuario y la imagen

        return RedirectToPage("/Index");
    }
}

}