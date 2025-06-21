using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.DAOs;

namespace Proyecto_Pastel.Pages.Postres
{
    public class CreateModel : PageModel
    {
        private readonly PostreDAO _postreDAO;

        public CreateModel()
        {
            _postreDAO = new PostreDAO();
        }

        [BindProperty]
        public postres NuevoPostre { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _postreDAO.Insertar(NuevoPostre);
            return RedirectToPage("/Sales/Index");
        }
    }
}
