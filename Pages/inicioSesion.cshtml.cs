using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Services;

namespace Proyecto_Pastel.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_userService.ValidarUsuario(Username, Password))
            {
                TempData["Mensaje"] = $"Bienvenido, {Username}";
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }
    }
}
