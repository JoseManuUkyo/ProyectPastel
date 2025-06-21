using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.DAOs;
using Proyecto_Pastel.Entities;

public class AgregarUsuarioModel : PageModel
{
    private readonly UsuarioDAO _usuarioDAO;

    public AgregarUsuarioModel(UsuarioDAO usuarioDAO)
    {
        _usuarioDAO = usuarioDAO;
    }

    [BindProperty]
    public usuarios Usuario { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _usuarioDAO.AgregarUsuario(Usuario);
        return RedirectToPage("/Index");
    }
}
