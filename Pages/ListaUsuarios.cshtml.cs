using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.DAOs;
using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.Pages
{
    public class ListaUsuariosModel : PageModel
    {
        private readonly UsuarioDAO _usuarioDAO;

        public ListaUsuariosModel(UsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }

        [BindProperty(SupportsGet = true)]
        public string Busqueda { get; set; }

        public List<usuarios> Usuarios { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(Busqueda))
            {
                Usuarios = _usuarioDAO.ObtenerTodos();
            }
            else
            {
                Usuarios = _usuarioDAO.BuscarPorNombre(Busqueda);
            }
        }
    }
}